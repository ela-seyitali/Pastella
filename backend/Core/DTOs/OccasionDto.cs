namespace Pastella.Backend.Core.DTOs
{
    public class OccasionDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string IconUrl { get; set; } = string.Empty;
        public List<string> SuggestedDecorations { get; set; } = new List<string>();
        public List<string> SuggestedColors { get; set; } = new List<string>();
    }
}