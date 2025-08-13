using Microsoft.AspNetCore.Mvc;
using Pastella.Backend.Core.DTOs;
using Pastella.Backend.Application.Services;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Pastella.Backend.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DesignsController : ControllerBase
    {
        private readonly IDesignService _designService;
        
        public DesignsController(IDesignService designService)
        {
            _designService = designService;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateDesign([FromBody] DesignCreateDto designDto)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new { message = "User ID not found or invalid." });
            }
            
            try
            {
                var design = await _designService.CreateDesign(designDto, userId);
                return CreatedAtAction(nameof(GetDesign), new { id = design.Id }, design);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDesign(int id)
        {
            var design = await _designService.GetDesign(id);
            if (design == null)
                return NotFound();
                
            return Ok(design);
        }
    }
} 