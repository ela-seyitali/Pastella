namespace Pastella.Backend.Core.DTOs
{
    public class CakeCustomizationDto
    {
        public int Id { get; set; }
        public string Size { get; set; } = string.Empty;
        public string Shape { get; set; } = string.Empty;
        public string Flavor { get; set; } = string.Empty;
        public int Layers { get; set; }
        public string PrimaryColor { get; set; } = string.Empty;
        public string SecondaryColor { get; set; } = string.Empty;
        public List<string> DecorationElements { get; set; } = new List<string>();
        public string CustomMessage { get; set; } = string.Empty;
        public string? PhotoCakeImageUrl { get; set; }
        public decimal BasePrice { get; set; }
        public decimal CustomizationPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}