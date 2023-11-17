using Base.Persistence;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Base.OpenIddict
{
    public class DefaultWorker : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public DefaultWorker(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            this._serviceProvider = serviceProvider;
            this._configuration = configuration;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = this._serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.EnsureCreatedAsync(cancellationToken);

            var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

            if (await manager.FindByClientIdAsync("web-client", cancellationToken) is null)
            {
                await manager.CreateAsync(
                    new OpenIddictApplicationDescriptor
                    {
                        ClientId = "web-client",
                        ClientSecret = this._configuration["WebClientSecret"],
                        DisplayName = "WebClient",
                        Permissions =
                        {
                            Permissions.Endpoints.Token,
                            Permissions.Endpoints.Logout,
                            Permissions.GrantTypes.Password,
                            Permissions.GrantTypes.RefreshToken,
                            Permissions.Prefixes.Scope + "api"                          
                        }
                    }, 
                    cancellationToken
                );
            }
            if (await manager.FindByClientIdAsync("swagger-client", cancellationToken) is null)
            {
                await manager.CreateAsync(
                    new OpenIddictApplicationDescriptor
                    {
                        ClientId = "swagger-client",
                        ClientSecret = this._configuration["SwaggerClientSecret"],
                        DisplayName = "SwaggerClient",
                        Permissions =
                        {
                            Permissions.Endpoints.Token,
                            Permissions.Endpoints.Logout,
                            Permissions.GrantTypes.Password,
                            Permissions.GrantTypes.RefreshToken,
                            Permissions.Prefixes.Scope + "api"
                        }
                    }, 
                    cancellationToken
                );
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
