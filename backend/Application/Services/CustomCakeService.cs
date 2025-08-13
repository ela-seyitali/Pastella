using Pastella.Backend.Core.DTOs;
using Pastella.Backend.Core.Entities;
using Pastella.Backend.Core.Interfaces;

namespace Pastella.Backend.Application.Services
{
    public class CustomCakeService : ICustomCakeService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IImageService _imageService;

        public CustomCakeService(IOrderRepository orderRepository, IImageService imageService)
        {
            _orderRepository = orderRepository;
            _imageService = imageService;
        }

        public async Task<OrderDto?> CreateCustomCake(CreateCustomCakeDto createCustomCakeDto, int userId)
        {
            // Calculate price
            var totalPrice = await CalculatePrice(
                createCustomCakeDto.Size,
                createCustomCakeDto.Shape,
                createCustomCakeDto.Layers,
                createCustomCakeDto.PhotoCakeImage != null,
                createCustomCakeDto.DecorationElements.Count
            );

            // Handle photo upload if exists
            string? photoCakeImageUrl = null;
            if (createCustomCakeDto.PhotoCakeImage != null)
            {
                photoCakeImageUrl = await _imageService.UploadImageAsync(createCustomCakeDto.PhotoCakeImage);
            }

            // Create order for custom cake
            var order = new Order
            {
                UserId = userId,
                CakeId = null, // Custom cake doesn't have a predefined cake
                TotalPrice = totalPrice,
                Status = "Pending",
                OrderDate = DateTime.UtcNow,
                DeliveryDate = createCustomCakeDto.DeliveryDate,
                DeliveryAddress = $"Address ID: {createCustomCakeDto.DeliveryAddressId}",
                Notes = $"Custom Cake - Size: {createCustomCakeDto.Size}, Shape: {createCustomCakeDto.Shape}, " +
                       $"Flavor: {createCustomCakeDto.Flavor}, Layers: {createCustomCakeDto.Layers}, " +
                       $"Colors: {createCustomCakeDto.PrimaryColor}/{createCustomCakeDto.SecondaryColor}, " +
                       $"Message: {createCustomCakeDto.CustomMessage}, " +
                       $"Decorations: {string.Join(", ", createCustomCakeDto.DecorationElements)}, " +
                       $"Special Instructions: {createCustomCakeDto.SpecialInstructions}, " +
                       $"Photo URL: {photoCakeImageUrl}"
            };

            await _orderRepository.Create(order);

            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                CakeId = order.CakeId, // Now nullable, so no conversion needed
                TotalPrice = order.TotalPrice,
                OrderDate = order.OrderDate,
                Status = order.Status
            };
        }

        public async Task<decimal> CalculatePrice(string size, string shape, int layers, bool hasPhotoCake, int decorationCount)
        {
            decimal basePrice = 0;

            // Size pricing
            basePrice += size.ToLower() switch
            {
                "small" => 150,
                "medium" => 250,
                "large" => 350,
                "extra-large" => 450,
                _ => 200
            };

            // Shape pricing
            basePrice += shape.ToLower() switch
            {
                "round" => 0,
                "square" => 25,
                "heart" => 50,
                "custom" => 100,
                _ => 0
            };

            // Layer pricing - fixed the null coalescing operator issue
            basePrice += Math.Max(0, layers - 1) * 75; // First layer included, additional layers cost 75 TL each

            // Photo cake pricing
            if (hasPhotoCake)
            {
                basePrice += 100;
            }

            // Decoration pricing
            basePrice += decorationCount * 25; // Each decoration element costs 25 TL

            return await Task.FromResult(basePrice);
        }

        public async Task<object> GetCustomizationOptions()
        {
            var options = new
            {
                Sizes = new[] { "Small", "Medium", "Large", "Extra-Large" },
                Shapes = new[] { "Round", "Square", "Heart", "Custom" },
                Flavors = new[] { "Vanilla", "Chocolate", "Strawberry", "Red Velvet", "Lemon", "Carrot" },
                Colors = new[] { "#FF6B6B", "#4ECDC4", "#45B7D1", "#96CEB4", "#FFEAA7", "#DDA0DD", "#98D8C8", "#F7DC6F" },
                DecorationElements = new[] { "Flowers", "Hearts", "Stars", "Butterflies", "Balloons", "Candles", "Pearls", "Ribbons" },
                MaxLayers = 5,
                MinLayers = 1
            };

            return await Task.FromResult(options);
        }
    }
}