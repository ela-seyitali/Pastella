namespace Pastella.Backend.Core.DTOs
{
    public class BakeryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsApproved { get; set; }
        public List<string> SocialMediaLinks { get; set; } = new List<string>();
    }
}