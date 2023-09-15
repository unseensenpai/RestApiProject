using Microsoft.AspNetCore.Mvc;


namespace TestProject.Contracts.Image
{
    public interface IImageService
    {
        public IActionResult GetImageWithId(string id);
    }
}
