using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pastella.Backend.Core.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Confirmed, InProgress, Ready, Delivered, Cancelled
        public DateTime? DeliveryDate { get; set; }
        public string DeliveryAddress { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public string? PaymentTransactionId { get; set; }
        public bool IsPaid { get; set; } = false;

        // Foreign Key for User
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        // Foreign Key for Cake (nullable for custom cakes)
        public int? CakeId { get; set; }
        [ForeignKey("CakeId")]
        public Cake? Cake { get; set; }
        
        // Foreign Key for Bakery (optional)
        public int? BakeryId { get; set; }
        [ForeignKey("BakeryId")]
        public Bakery? Bakery { get; set; }
    }
}