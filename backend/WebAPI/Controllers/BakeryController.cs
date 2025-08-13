using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Pastella.Backend.Core.DTOs;
using Pastella.Backend.Application.Services;

namespace Pastella.Backend.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BakeryController : ControllerBase
    {
        private readonly IBakeryService _bakeryService;

        public BakeryController(IBakeryService bakeryService)
        {
            _bakeryService = bakeryService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterBakery([FromBody] CreateBakeryDto createBakeryDto)
        {
            var result = await _bakeryService.CreateBakery(createBakeryDto);
            if (result != null)
            {
                return Ok(new { Message = "Pastane başvurusu alındı, onay bekleniyor", BakeryId = result.Id });
            }
            return BadRequest(new { Message = "Pastane kaydı başarısız" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBakeries()
        {
            var bakeries = await _bakeryService.GetAllBakeries();
            return Ok(bakeries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBakery(int id)
        {
            var bakery = await _bakeryService.GetBakeryById(id);
            if (bakery != null)
            {
                return Ok(bakery);
            }
            return NotFound(new { Message = "Pastane bulunamadı" });
        }

        [HttpGet("{id}/orders")]
        [Authorize(Roles = "Bakery,Admin")]
        public async Task<IActionResult> GetBakeryOrders(int id)
        {
            var orders = await _bakeryService.GetBakeryOrders(id);
            return Ok(orders);
        }
    }
}