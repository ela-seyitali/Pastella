using Microsoft.AspNetCore.Http;

namespace Pastella.Backend.Core.DTOs
{
    public class CreateCustomCakeDto
    {
        public string Size { get; set; } = string.Empty;
        public string Shape { get; set; } = string.Empty;
        public string Flavor { get; set; } = string.Empty;
        public int Layers { get; set; } = 1;
        public string PrimaryColor { get; set; } = string.Empty;
        public string SecondaryColor { get; set; } = string.Empty;
        public List<string> DecorationElements { get; set; } = new List<string>();
        public string CustomMessage { get; set; } = string.Empty;
        public IFormFile? PhotoCakeImage { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int DeliveryAddressId { get; set; }
        public string? SpecialInstructions { get; set; }
    }
}