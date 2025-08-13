using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pastella.Backend.Core.Entities
{
    public class DesignImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        // Foreign Key for SweetDesign
        public int SweetDesignId { get; set; }
        [ForeignKey("SweetDesignId")]
        public SweetDesign SweetDesign { get; set; } = null!;
    }
} 