using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Pastella.Backend.Core.DTOs;
using Pastella.Backend.Application.Services;
using System.Security.Claims;

namespace Pastella.Backend.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CustomCakeController : ControllerBase
    {
        private readonly ICustomCakeService _customCakeService;

        public CustomCakeController(ICustomCakeService customCakeService)
        {
            _customCakeService = customCakeService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCustomCake([FromForm] CreateCustomCakeDto createCustomCakeDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var result = await _customCakeService.CreateCustomCake(createCustomCakeDto, userId);
            
            if (result != null)
            {
                return Ok(new { 
                    Message = "Özel pasta tasarımınız oluşturuldu! 🎂", 
                    OrderId = result.Id,
                    EstimatedPrice = result.TotalPrice 
                });
            }
            return BadRequest(new { Message = "Pasta tasarımı oluşturulamadı" });
        }

        [HttpGet("price-calculator")]
        public async Task<IActionResult> CalculatePrice([FromQuery] string size, [FromQuery] string shape, 
            [FromQuery] int layers, [FromQuery] bool hasPhotoCake, [FromQuery] int decorationCount)
        {
            var price = await _customCakeService.CalculatePrice(size, shape, layers, hasPhotoCake, decorationCount);
            return Ok(new { EstimatedPrice = price });
        }

        [HttpGet("customization-options")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCustomizationOptions()
        {
            var options = await _customCakeService.GetCustomizationOptions();
            return Ok(options);
        }
    }
}