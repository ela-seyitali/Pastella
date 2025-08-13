using System.ComponentModel.DataAnnotations;

namespace Pastella.Backend.Core.DTOs
{
    public class RefreshTokenDto
    {
        [Required]
        public string RefreshToken { get; set; } = string.Empty;
    }
}