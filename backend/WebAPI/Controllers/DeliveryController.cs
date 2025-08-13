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
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService _deliveryService;

        public DeliveryController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        [HttpPost("addresses")]
        public async Task<IActionResult> AddDeliveryAddress([FromBody] DeliveryAddressDto addressDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var result = await _deliveryService.AddDeliveryAddress(addressDto, userId);
            
            if (result != null)
            {
                return Ok(new { Message = "Teslimat adresi eklendi", AddressId = result.Id });
            }
            return BadRequest(new { Message = "Adres eklenemedi" });
        }

        [HttpGet("addresses")]
        public async Task<IActionResult> GetUserAddresses()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var addresses = await _deliveryService.GetUserAddresses(userId);
            return Ok(addresses);
        }

        [HttpPut("addresses/{id}/default")]
        public async Task<IActionResult> SetDefaultAddress(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var result = await _deliveryService.SetDefaultAddress(id, userId);
            
            if (result)
            {
                return Ok(new { Message = "Varsayılan adres güncellendi" });
            }
            return NotFound(new { Message = "Adres bulunamadı" });
        }

        [HttpGet("time-slots")]
        public async Task<IActionResult> GetAvailableTimeSlots([FromQuery] DateTime date)
        {
            var timeSlots = await _deliveryService.GetAvailableTimeSlots(date);
            return Ok(timeSlots);
        }
    }
}