namespace Pastella.Backend.Core.DTOs
{
    public class CreateBakeryDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<string> SocialMediaLinks { get; set; } = new List<string>();
    }
}