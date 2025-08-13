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
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var result = await _orderService.CreateOrder(createOrderDto, userId);
            
            if (result != null)
            {
                return Ok(new { 
                    Message = "Siparişiniz alındı! 🎉", 
                    OrderId = result.Id,
                    TrackingNumber = result.Id.ToString("D8")
                });
            }
            return BadRequest(new { Message = "Sipariş oluşturulamadı" });
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserOrders(int userId)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            
            if (currentUserId != userId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            var orders = await _orderService.GetUserOrders(userId);
            return Ok(orders);
        }

        [HttpGet("{id}/track")]
        public async Task<IActionResult> TrackOrder(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var order = await _orderService.GetOrderById(id);
            
            if (order == null)
            {
                return NotFound(new { Message = "Sipariş bulunamadı" });
            }

            // Kullanıcı sadece kendi siparişlerini takip edebilir
            if (order.UserId != userId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            var trackingInfo = await _orderService.GetOrderTrackingInfo(id);
            return Ok(trackingInfo);
        }

        [HttpPatch("{id}/status")]
        [Authorize(Roles = "Admin,Bakery")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] string status)
        {
            var result = await _orderService.UpdateOrderStatus(id, status);
            if (result)
            {
                return Ok(new { Message = "Sipariş durumu güncellendi" });
            }
            return NotFound(new { Message = "Sipariş bulunamadı" });
        }

        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var result = await _orderService.CancelOrder(id, userId);
            
            if (result)
            {
                return Ok(new { Message = "Sipariş iptal edildi" });
            }
            return BadRequest(new { Message = "Sipariş iptal edilemedi" });
        }
    }
}