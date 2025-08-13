using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pastella.Backend.Core.Entities
{
    public class Cake
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        // Foreign Key for SweetDesign
        public int SweetDesignId { get; set; }
        [ForeignKey("SweetDesignId")]
        public SweetDesign SweetDesign { get; set; } = null!;
        
        // Navigation Properties
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}