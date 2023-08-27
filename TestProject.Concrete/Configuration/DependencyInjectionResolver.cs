using TestProject.BL.Configuration;
using TestProject.Concrete.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionResolver
    {
        public static IServiceCollection AddServiceModules(this IServiceCollection services) =>
            services
                .AddAuthModule()
                .AddEmployeeModule()
                .AddImageModule()
                .AddCoreModule();
    }
}
