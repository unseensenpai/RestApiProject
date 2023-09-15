using Microsoft.Extensions.DependencyInjection;
using TestProject.BL.Core;
using TestProject.Contracts.Core;

namespace TestProject.BL.Configuration
{
    public static class CoreConfigurationExtensions
    {
        public static IServiceCollection AddCoreModule(this IServiceCollection services) =>
            services.AddTransient<IBenchmarkRun, BenchmarkRun>();
    }
}
