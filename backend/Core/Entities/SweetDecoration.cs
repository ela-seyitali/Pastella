using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pastella.Backend.Core.Entities
{
    public class SweetDecoration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int SweetDesignId { get; set; }
        public SweetDesign SweetDesign { get; set; } = null!;

        public int DecorationId { get; set; }
        public Decoration Decoration { get; set; } = null!;

        public string PositionJson { get; set; } = string.Empty; // (x, y) pozisyonu
    }
}