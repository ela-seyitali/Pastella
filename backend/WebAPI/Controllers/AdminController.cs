using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Pastella.Backend.Application.Services;

namespace Pastella.Backend.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IBakeryService _bakeryService;

        public AdminController(IUserService userService, IBakeryService bakeryService)
        {
            _userService = userService;
            _bakeryService = bakeryService;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpPut("bakeries/{id}/approve")]
        public async Task<IActionResult> ApproveBakery(int id, [FromBody] bool approve)
        {
            var result = await _bakeryService.ApproveBakery(id, approve);
            if (result)
            {
                var message = approve ? "Pastane onaylandı" : "Pastane reddedildi";
                return Ok(new { Message = message });
            }
            return NotFound(new { Message = "Pastane bulunamadı" });
        }

        [HttpGet("bakeries/pending")]
        public async Task<IActionResult> GetPendingBakeries()
        {
            var bakeries = await _bakeryService.GetPendingBakeries();
            return Ok(bakeries);
        }
    }
}