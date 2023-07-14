using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TestProject.Concrete.Auth;
using TestProject.Contracts.Auth;

namespace TestProject.Concrete.Configuration
{
    public static class AuthConfigurationExtensions
    {
        public static IServiceCollection AddAuthModule(this IServiceCollection services) =>
            services
                .AddTransient<IAuthService, AuthService>()
                .AddTransient<TokenValidationParameters>();
    }
}

