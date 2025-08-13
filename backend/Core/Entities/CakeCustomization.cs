using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pastella.Backend.Core.Entities
{
    public class CakeCustomization
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        // Temel Özellikler
        public string Size { get; set; } = string.Empty; // Small, Medium, Large, XL
        public string Shape { get; set; } = string.Empty; // Round, Square, Heart, Custom
        public string Flavor { get; set; } = string.Empty; // Chocolate, Vanilla, Strawberry, etc.
        public int Layers { get; set; } = 1;
        
        // Renk ve Dekorasyon
        public string PrimaryColor { get; set; } = string.Empty;
        public string SecondaryColor { get; set; } = string.Empty;
        public List<string> DecorationElements { get; set; } = new List<string>();
        
        // Özel Mesaj ve Fotoğraf
        public string CustomMessage { get; set; } = string.Empty;
        public string? PhotoCakeImageUrl { get; set; }
        
        // Fiyatlandırma
        public decimal BasePrice { get; set; }
        public decimal CustomizationPrice { get; set; }
        public decimal TotalPrice { get; set; }
        
        // İlişkiler
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; } = null!;
    }
}