using Pastella.Backend.Core.DTOs;
using Pastella.Backend.Core.Entities;
using Pastella.Backend.Core.Interfaces;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Pastella.Backend.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        
        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<AuthResult> Register(UserRegistrationDto registrationDto)
        {
            // Kullanıcı var mı kontrolü
            var existingUser = await _userRepository.GetByEmail(registrationDto.Email);
            if (existingUser != null)
                return new AuthResult { Success = false, Errors = new[] { "Bu email adresi zaten kullanılıyor" } };

            // Yeni kullanıcı oluştur
            var user = new User
            {
                FullName = registrationDto.FullName,
                Email = registrationDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registrationDto.Password),
                Role = "User",
                CreatedDate = DateTime.UtcNow,
                IsEmailVerified = false
            };

            await _userRepository.Create(user);

            var token = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();
            
            // Refresh token'ı kullanıcıya kaydet
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpires = DateTime.UtcNow.AddDays(30);
            await _userRepository.Update(user.Id, user);

            return new AuthResult { Success = true, Token = token, RefreshToken = refreshToken };
        }

        public async Task<AuthResult> Login(UserLoginDto loginDto)
        {
            var user = await _userRepository.GetByEmail(loginDto.Email);
            if (user == null)
                return new AuthResult { Success = false, Errors = new[] { "Invalid email or password" } };

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                return new AuthResult { Success = false, Errors = new[] { "Invalid email or password" } };

            var token = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();
            
            // Refresh token'ı kullanıcıya kaydet
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpires = DateTime.UtcNow.AddDays(30);
            await _userRepository.Update(user.Id, user);

            return new AuthResult { Success = true, Token = token, RefreshToken = refreshToken };
        }

        public async Task<AuthResult> ForgotPassword(string email)
        {
            var user = await _userRepository.GetByEmail(email);
            if (user == null)
                return new AuthResult { Success = false, Errors = new[] { "Bu email adresi ile kayıtlı kullanıcı bulunamadı" } };

            // Reset token oluştur
            user.ResetPasswordToken = Guid.NewGuid().ToString();
            user.ResetPasswordExpires = DateTime.UtcNow.AddHours(1);

            await _userRepository.Update(user.Id, user);

            // Burada email gönderme işlemi yapılabilir
            // await _emailService.SendPasswordResetEmail(user.Email, user.ResetPasswordToken);

            return new AuthResult { Success = true };
        }

        public async Task<AuthResult> ResetPassword(string token, string newPassword)
        {
            var user = await _userRepository.GetByResetToken(token);
            if (user == null || user.ResetPasswordExpires < DateTime.UtcNow)
                return new AuthResult { Success = false, Errors = new[] { "Geçersiz veya süresi dolmuş token" } };

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.ResetPasswordToken = null;
            user.ResetPasswordExpires = null;

            await _userRepository.Update(user.Id, user);

            return new AuthResult { Success = true };
        }

        public async Task<AuthResult> RefreshToken(string refreshToken)
        {
            var user = await _userRepository.GetByRefreshToken(refreshToken);
            if (user == null || user.RefreshTokenExpires < DateTime.UtcNow)
                return new AuthResult { Success = false, Errors = new[] { "Geçersiz veya süresi dolmuş refresh token" } };

            var newToken = GenerateJwtToken(user);
            var newRefreshToken = GenerateRefreshToken();
            
            // Yeni refresh token'ı kaydet
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpires = DateTime.UtcNow.AddDays(30);
            await _userRepository.Update(user.Id, user);

            return new AuthResult { Success = true, Token = newToken, RefreshToken = newRefreshToken };
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured"));
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}