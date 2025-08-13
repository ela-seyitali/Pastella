using System.Collections.Generic;

namespace Pastella.Backend.Core.DTOs
{
    public class DesignCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Shape { get; set; } = string.Empty;
        public List<string> Layers { get; set; } = new List<string>();
        public List<DesignDecorationDto> Decorations { get; set; } = new List<DesignDecorationDto>();
        public string ColorHex { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}