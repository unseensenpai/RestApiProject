using Microsoft.Extensions.DependencyInjection;
using TestProject.Concrete.Image;
using TestProject.Contracts.Image;

namespace TestProject.Concrete.Configuration
{
    public static class ImageConfigurationExtensions
    {
        public static IServiceCollection AddImageModule(this IServiceCollection services)
        {
            services.AddTransient<IImageService, ImageService>();
            return services;
        }
    }
}
