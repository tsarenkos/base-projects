using Base.Application.Common.Interfaces;
using Base.Common;
using Base.Domain.Entities;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System.Security.Claims;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Base.OpenIddict.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IApplicationDbContext _dbContext;

        public AuthorizationController(IApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost("/connect/token"), Produces("application/json")]
        public async Task<IActionResult> Exchange()
        {
            var request = this.HttpContext.GetOpenIddictServerRequest() ??
                throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

            if (request.IsPasswordGrantType())
            {
                return await this.SignInPasswordGrantType(request);
            }
            else if (request.IsRefreshTokenGrantType())
            {
                return await this.SignInRefreshTokenGrantType(request);
            }

            throw new NotImplementedException("The specified grant type is not implemented");
        }

        private async Task<IActionResult> SignInPasswordGrantType(OpenIddictRequest request)
        {
            var user = await this._dbContext.Users.FirstOrDefaultAsync(x => x.Email == request.Username);

            if (user is null
                || !CryptoHelper.VerifyHashedPassword(user.Password, request.Password))
            {
                var properties = new AuthenticationProperties(
                    new Dictionary<string, string>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The username/password couple is invalid."
                    }!
                );

                return this.Forbid(properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            var claimsIdentity = this.GetClaimsIdeintity(user, request);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            return this.SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        private async Task<IActionResult> SignInRefreshTokenGrantType(OpenIddictRequest request)
        {
            var result = await this.HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            if (result is null || result.Principal is null)
            {
                var properties = new AuthenticationProperties(new Dictionary<string, string>
                {
                    [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
                    [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The refresh token is no longer valid."
                }!);

                return this.Forbid(properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }
            
            Guid.TryParse(result.Principal.GetClaim(Claims.Subject), out var id);
            var user = await this._dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user is null)
            {
                var properties = new AuthenticationProperties(new Dictionary<string, string>
                {
                    [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
                    [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The refresh token is no longer valid."
                }!);

                return this.Forbid(properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            var claimsIdentity = this.GetClaimsIdeintity(user, request);          
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            return this.SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        private ClaimsIdentity GetClaimsIdeintity(User user, OpenIddictRequest request)
        {
            var claimsIdentity = new ClaimsIdentity(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme, Claims.Name, Claims.Role);

            claimsIdentity.AddClaims(new List<Claim>
            {
                new Claim(Claims.Subject, user.Id.ToString()),
                new Claim(Claims.Email, user.Email),
                new Claim(Claims.Role, user.UserRole.ToString()),                
            });

            claimsIdentity.SetScopes(new[]
            {
                Scopes.OfflineAccess,
                Scopes.OpenId,
                Scopes.Email,
                Scopes.Profile,
                Scopes.Roles
            }.Intersect(request.GetScopes()));

            foreach (var claim in claimsIdentity.Claims)
            {
                var destinations = this.GetDestinations(claim);
                claim.SetDestinations(destinations);
            }            

            return claimsIdentity;
        }

        private IEnumerable<string> GetDestinations(Claim claim)
        {
            switch (claim.Type)
            {
                case Claims.Name:
                    yield return Destinations.AccessToken;

                    if (claim.Subject.HasScope(Scopes.Profile))
                    {
                        yield return Destinations.IdentityToken;
                    }                        

                    yield break;

                case Claims.Email:
                    yield return Destinations.AccessToken;

                    if (claim.Subject.HasScope(Scopes.Email))
                    {
                        yield return Destinations.IdentityToken;
                    }                        

                    yield break;

                case Claims.Role:
                    yield return Destinations.AccessToken;

                    if (claim.Subject.HasScope(Scopes.Roles))
                    {
                        yield return Destinations.IdentityToken;
                    }                        

                    yield break;                

                case "AspNet.Identity.SecurityStamp": 
                    yield break;

                default:
                    yield return Destinations.AccessToken;
                    yield break;
            }
        }
    }
}
