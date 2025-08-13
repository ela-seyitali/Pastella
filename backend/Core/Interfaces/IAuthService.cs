using Pastella.Backend.Core.DTOs;
using System.Threading.Tasks;

namespace Pastella.Backend.Application.Services
{
    public interface IAuthService
    {
        Task<AuthResult> Register(UserRegistrationDto registrationDto);
        Task<AuthResult> Login(UserLoginDto loginDto);
        Task<AuthResult> ForgotPassword(string email);
        Task<AuthResult> ResetPassword(string token, string newPassword);
        Task<AuthResult> RefreshToken(string refreshToken);
    }
}