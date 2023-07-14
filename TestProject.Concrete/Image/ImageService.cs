using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TestProject.Contracts.Image;

namespace TestProject.Concrete.Image
{
    public class ImageService : IImageService
    {
        private readonly IConfiguration _configuration;

        public ImageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult GetImageWithId(string id)
        {
            var base64 = _configuration.GetValue<string>($"Image:Base64:{id}") ?? string.Empty;
            var bytes = Convert.FromBase64String(base64);

            using var ms = new MemoryStream(bytes, 0, bytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            ArgumentNullException.ThrowIfNull(image, nameof(image));
            return new ContentResult() { Content = base64, ContentType = "image/jpeg", StatusCode = 200 };
        }
    }
}
