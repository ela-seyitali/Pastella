using Microsoft.AspNetCore.Mvc;
using Pastella.Backend.Application.Services;
using Pastella.Backend.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pastella.Backend.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CakeController : ControllerBase
    {
        private readonly ICakeService _cakeService;

        public CakeController(ICakeService cakeService)
        {
            _cakeService = cakeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CakeDto>>> GetAllCakes()
        {
            var cakes = await _cakeService.GetAllAsync();
            return Ok(cakes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CakeDto>> GetCakeById(int id)
        {
            var cake = await _cakeService.GetByIdAsync(id);
            if (cake == null)
            {
                return NotFound();
            }
            return Ok(cake);
        }

        [HttpPost]
        public async Task<ActionResult<CakeDto>> CreateCake([FromBody] CreateCakeDto dto)
        {
            var createdCake = await _cakeService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetCakeById), new { id = createdCake.Id }, createdCake);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCake(int id, [FromBody] CreateCakeDto dto)
        {
            try
            {
                await _cakeService.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCake(int id)
        {
            try
            {
                await _cakeService.DeleteAsync(id);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return NotFound(ex.Message); // Cake not found should result in NotFound
            }
        }
    }
} 