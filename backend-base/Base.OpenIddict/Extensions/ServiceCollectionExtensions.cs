using Base.Persistence;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Validation.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Base.OpenIddict.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureServices(
            this IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            services.AddControllersWithViews();
            services.AddPersistence(configuration);

            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = Claims.Role;
                options.ClaimsIdentity.EmailClaimType = Claims.Email;
            });

            services.AddOpenIddict()
                .AddCore(options =>
                {
                    options.UseEntityFrameworkCore()
                            .UseDbContext<ApplicationDbContext>()
                            .ReplaceDefaultEntities<Guid>();
                })
                .AddServer(options =>
                {
                    options.SetTokenEndpointUris("connect/token")
                            .SetLogoutEndpointUris("connect/logout");

                    options.AllowPasswordFlow()
                            .AllowRefreshTokenFlow();

                    options.RegisterScopes("api");

                    if (environment.IsDevelopment())
                    {
                        options.AddDevelopmentEncryptionCertificate()
                                .AddDevelopmentSigningCertificate();
                    }
                    else
                    {
                        options.AddSigningCertificate(configuration["SigningCertificateThumbprint"]);
                    }

                    options.AddEncryptionKey(
                        new SymmetricSecurityKey(
                            Convert.FromBase64String(configuration["JWTEncryptionKey"]))
                        );

                    options.SetAccessTokenLifetime(TimeSpan.FromMinutes(5));
                    options.SetRefreshTokenLifetime(TimeSpan.FromHours(24));

                    options.UseAspNetCore()
                            .EnableTokenEndpointPassthrough()
                            .EnableLogoutEndpointPassthrough();
                })
                .AddValidation(options =>
                {
                    options.UseLocalServer();
                    options.UseAspNetCore();
                });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
            });

            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: "BaseCorsPolicy",
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                    }
                );
            });

            services.AddHostedService<DefaultWorker>();

            return services;
        }
    }
}
