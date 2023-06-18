using Microsoft.AspNetCore.Mvc;
using TestProject.Contracts.Image;
using TestProject.HttpApi.Core;

namespace TestProject.HttpApi.Image
{
    public class ImageController : CoreController, IImageService
    {
        private readonly IImageService _imageService;
        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet]
        public IActionResult GetImageWithId([FromQuery] string id)
        {
            return _imageService.GetImageWithId(id);
        }
    }
}
