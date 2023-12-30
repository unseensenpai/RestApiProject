using Microsoft.Extensions.DependencyInjection;
using RestApiProject.DAL.Datalayers;
using RestApiProject.DAL.DataLayers;

namespace TestProject.Concrete.Configuration
{
    public static class DatabaseLayersConfigurationExtensions
    {
        public static IServiceCollection AddDatabaseLayers(this IServiceCollection services) => 
            services
                .AddTransient<IRestApiDataLayer, RestApiThreadSafeDataLayer>();
    }
}
