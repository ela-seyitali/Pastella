using Microsoft.AspNetCore.Mvc;
using Pastella.Backend.Application.Services;

namespace Pastella.Backend.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OccasionController : ControllerBase
    {
        private readonly IOccasionService _occasionService;

        public OccasionController(IOccasionService occasionService)
        {
            _occasionService = occasionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOccasions()
        {
            var occasions = await _occasionService.GetAllOccasions();
            return Ok(occasions);
        }

        [HttpGet("{id}/suggestions")]
        public async Task<IActionResult> GetOccasionSuggestions(int id)
        {
            var suggestions = await _occasionService.GetOccasionSuggestions(id);
            if (suggestions != null)
            {
                return Ok(suggestions);
            }
            return NotFound(new { Message = "Özel gün bulunamadı" });
        }
    }
}