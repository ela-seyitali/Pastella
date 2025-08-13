using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pastella.Backend.Application.Services;
using System.Security.Claims;

namespace Pastella.Backend.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FCMController : ControllerBase
    {
        private readonly IFCMService _fcmService;

        public FCMController(IFCMService fcmService)
        {
            _fcmService = fcmService;
        }

        [HttpPost("register-token")]
        public async Task<IActionResult> RegisterDeviceToken([FromBody] RegisterTokenRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized();
            }

            var result = await _fcmService.RegisterDeviceToken(userId, request.Token, request.DeviceType);
            
            if (result)
                return Ok(new { message = "Device token registered successfully" });
            
            return BadRequest(new { message = "Failed to register device token" });
        }

        [HttpPost("unregister-token")]
        public async Task<IActionResult> UnregisterDeviceToken([FromBody] UnregisterTokenRequest request)
        {
            var result = await _fcmService.UnregisterDeviceToken(request.Token);
            
            if (result)
                return Ok(new { message = "Device token unregistered successfully" });
            
            return BadRequest(new { message = "Failed to unregister device token" });
        }

        [HttpPost("test-notification")]
        public async Task<IActionResult> SendTestNotification()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized();
            }

            var result = await _fcmService.SendToUser(userId, "Test Bildirimi", "Bu bir test bildirimidir!");
            
            if (result)
                return Ok(new { message = "Test notification sent successfully" });
            
            return BadRequest(new { message = "Failed to send test notification" });
        }
    }

    public class RegisterTokenRequest
    {
        public string Token { get; set; } = string.Empty;
        public string DeviceType { get; set; } = string.Empty;
    }

    public class UnregisterTokenRequest
    {
        public string Token { get; set; } = string.Empty;
    }
}