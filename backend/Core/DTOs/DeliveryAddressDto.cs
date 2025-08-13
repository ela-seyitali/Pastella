namespace Pastella.Backend.Core.DTOs
{
    public class DeliveryAddressDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string FullAddress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;
        public string? DeliveryNotes { get; set; }
        public bool IsDefault { get; set; }
    }
}