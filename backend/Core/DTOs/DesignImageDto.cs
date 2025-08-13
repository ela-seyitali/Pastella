namespace Pastella.Backend.Core.DTOs
{
    public class DesignImageDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int SweetDesignId { get; set; }
    }
} 