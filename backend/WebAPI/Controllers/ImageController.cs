using Microsoft.AspNetCore.Mvc;
using Pastella.Backend.Application.Services;
using Pastella.Backend.Core.DTOs;

namespace Pastella.Backend.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Dosya seçilmedi");

            var result = await _imageService.UploadImageAsync(file);
            return Ok(new { ImageUrl = result });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            var images = await _imageService.GetAllAsync();
            return Ok(images);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            var image = await _imageService.GetByIdAsync(id);
            if (image == null)
                return NotFound();
            return Ok(image);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            await _imageService.DeleteAsync(id);
            return NoContent();
        }
    }
}