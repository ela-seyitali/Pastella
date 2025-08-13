using System;

namespace Pastella.Backend.Core.DTOs
{
    public class CreateOrderDto
    {
        public int UserId { get; set; }
        public int CakeId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string? DeliveryAddress { get; set; }
        public string? Notes { get; set; }
    }
}