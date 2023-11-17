using Base.ApiServer.Filters;
using Base.Application;
using Base.Persistence;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using OpenIddict.Validation.AspNetCore;
using System.IdentityModel.Tokens.Jwt;

namespace Base.ApiServer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ApiExceptionFilterAttribute));
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver =
                        new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

            services.AddPersistence(configuration);
            services.AddApplication();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

            services.AddOpenIddict()
                .AddValidation(options =>
                {
                    options.SetIssuer(configuration["OpenIddictSettings:Authority"]);
                    options.AddEncryptionKey(
                        new SymmetricSecurityKey(Convert.FromBase64String(configuration["OpenIddictSettings:JWTEncryptionKey"])));
                    options.UseSystemNetHttp();
                    options.UseAspNetCore();
                });

            services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    "v1",
                    new OpenApiInfo { Title = "Base API Project", Version = "v1" }
                );

                options.AddSecurityDefinition(
                    "OAuth2",
                    new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.OAuth2,
                        Flows = new OpenApiOAuthFlows
                        {
                            Password = new OpenApiOAuthFlow
                            {
                                TokenUrl = new Uri($"{configuration["OpenIddictSettings:Authority"]}/connect/token")
                            }
                        }
                    }
                );

                options.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "OAuth2"
                                }
                            },
                            Array.Empty<string>()
                        }
                    }
                );
            });

            services.AddSwaggerGenNewtonsoftSupport();

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

            return services;
        }
    }
}
