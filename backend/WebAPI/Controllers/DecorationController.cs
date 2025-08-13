using Microsoft.AspNetCore.Mvc;
using Pastella.Backend.Application.Services;
using Pastella.Backend.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pastella.Backend.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DecorationController : ControllerBase
    {
        private readonly IDecorationService _decorationService;

        public DecorationController(IDecorationService decorationService)
        {
            _decorationService = decorationService;
        }

        [HttpGet]
        public async Task<ActionResult<List<DecorationDto>>> GetAllDecorations()
        {
            var decorations = await _decorationService.GetAllAsync();
            return Ok(decorations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DecorationDto>> GetDecorationById(int id)
        {
            var decoration = await _decorationService.GetByIdAsync(id);
            if (decoration == null)
            {
                return NotFound();
            }
            return Ok(decoration);
        }

        [HttpPost]
        public async Task<ActionResult<DecorationDto>> CreateDecoration([FromBody] AddDecorationDto dto)
        {
            try
            {
                var createdDecoration = await _decorationService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetDecorationById), new { id = createdDecoration.Id }, createdDecoration);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDecoration(int id, [FromBody] AddDecorationDto dto)
        {
            try
            {
                await _decorationService.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDecoration(int id)
        {
            try
            {
                await _decorationService.DeleteAsync(id);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
} 