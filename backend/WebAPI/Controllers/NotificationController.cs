using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Pastella.Backend.Application.Services;
using System.Security.Claims;

namespace Pastella.Backend.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserNotifications(int userId)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            
            // Kullanıcı sadece kendi bildirimlerini görebilir
            if (currentUserId != userId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            var notifications = await _notificationService.GetUserNotifications(userId);
            return Ok(notifications);
        }

        [HttpPatch("{id}/read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var result = await _notificationService.MarkAsRead(id);
            if (result)
            {
                return Ok(new { Message = "Bildirim okundu olarak işaretlendi" });
            }
            return NotFound(new { Message = "Bildirim bulunamadı" });
        }
    }
}