using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pastella.Backend.Core.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "User"; // User, Admin, Bakery
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsEmailVerified { get; set; } = false;
        public string? ResetPasswordToken { get; set; }
        public DateTime? ResetPasswordExpires { get; set; }
        
        // Refresh Token Fields
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpires { get; set; }
        
        // Navigation Properties
        public List<Order> Orders { get; set; } = new List<Order>();
        public List<SweetDesign> Designs { get; set; } = new List<SweetDesign>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Notification> Notifications { get; set; } = new List<Notification>();
    }
}