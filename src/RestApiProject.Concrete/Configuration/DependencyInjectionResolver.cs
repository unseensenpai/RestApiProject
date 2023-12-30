using TestProject.BL.Configuration;
using TestProject.Concrete.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionResolver
    {
        public static IServiceCollection AddServiceModules(this IServiceCollection services) =>
            services
                .AddDatabaseLayers()
                .AddAuthModule()
                .AddEmployeeModule()
                .AddImageModule()
                .AddUserModule()
                .AddCoreModule();
    }
}
