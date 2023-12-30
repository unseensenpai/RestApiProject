using Microsoft.Extensions.DependencyInjection;
using RestApiProject.BL.User;
using RestApiProject.Contracts.User;

namespace TestProject.Concrete.Configuration
{
    public static class UserConfigurationExtensions
    {
        public static IServiceCollection AddUserModule(this IServiceCollection services) => 
            services
                .AddTransient<IUserService, UserService>();
    }
}
