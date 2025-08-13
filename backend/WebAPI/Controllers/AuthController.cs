using Microsoft.AspNetCore.Mvc;
using Pastella.Backend.Core.DTOs;
using Pastella.Backend.Application.Services;
using System.Threading.Tasks;

namespace Pastella.Backend.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto registrationDto)
        {
            var result = await _authService.Register(registrationDto);
            if (result.Success)
            {
                return Ok(new { Token = result.Token, RefreshToken = result.RefreshToken, Message = "Kayıt başarılı" });
            }
            return BadRequest(new { Errors = result.Errors });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            var result = await _authService.Login(loginDto);
            if (result.Success)
            {
                return Ok(new { Token = result.Token, RefreshToken = result.RefreshToken, Message = "Giriş başarılı" });
            }
            return Unauthorized(new { Errors = result.Errors });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            var result = await _authService.ForgotPassword(forgotPasswordDto.Email);
            if (result.Success)
            {
                return Ok(new { Message = "Şifre sıfırlama bağlantısı email adresinize gönderildi" });
            }
            return BadRequest(new { Errors = result.Errors });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            var result = await _authService.ResetPassword(resetPasswordDto.Token, resetPasswordDto.NewPassword);
            if (result.Success)
            {
                return Ok(new { Message = "Şifre başarıyla sıfırlandı" });
            }
            return BadRequest(new { Errors = result.Errors });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var result = await _authService.RefreshToken(request.RefreshToken);
            if (result.Success)
            {
                return Ok(new { Token = result.Token, RefreshToken = result.RefreshToken, Message = "Token yenilendi" });
            }
            return Unauthorized(new { Errors = result.Errors });
        }
    }
}