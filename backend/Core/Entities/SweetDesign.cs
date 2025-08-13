using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pastella.Backend.Core.Entities
{
    public class SweetDesign
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CreatedByUserId { get; set; }
        public string Shape { get; set; } = string.Empty;
        public List<string> Layers { get; set; } = new List<string>();
        public ICollection<SweetDecoration> SweetDecorations { get; set; } = new List<SweetDecoration>();
        public string ColorHex { get; set; } = string.Empty;
        public decimal Price { get; set; }
        
        // Navigation property - sadece CreatedByUser kullanıyoruz
        public User? CreatedByUser { get; set; }
    }
}