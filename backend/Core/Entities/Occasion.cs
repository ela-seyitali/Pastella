using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pastella.Backend.Core.Entities
{
    public class Occasion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // Birthday, Wedding, Anniversary, etc.
        public string Description { get; set; } = string.Empty;
        public string IconUrl { get; set; } = string.Empty;
        public List<string> SuggestedDecorations { get; set; } = new List<string>();
        public List<string> SuggestedColors { get; set; } = new List<string>();
        public bool IsActive { get; set; } = true;
    }
}