using Pastella.Backend.Core.DTOs;
using Pastella.Backend.Core.Entities;
using Pastella.Backend.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pastella.Backend.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICakeRepository _cakeRepository;
        private readonly INotificationService _notificationService;
        private readonly IFCMService _fcmService;

        public OrderService(IOrderRepository orderRepository, IUserRepository userRepository, 
                       ICakeRepository cakeRepository, INotificationService notificationService, 
                       IFCMService fcmService)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _cakeRepository = cakeRepository;
            _notificationService = notificationService;
            _fcmService = fcmService;
        }

        public async Task<OrderDto> CreateAsync(CreateOrderDto dto)
        {
            var order = new Order
            {
                UserId = dto.UserId,
                CakeId = dto.CakeId,
                TotalPrice = dto.TotalPrice,
                Status = "Pending",
                OrderDate = DateTime.UtcNow,
                DeliveryDate = dto.DeliveryDate,
                DeliveryAddress = dto.DeliveryAddress ?? string.Empty,
                Notes = dto.Notes ?? string.Empty
            };

            await _orderRepository.Create(order);

            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                CakeId = order.CakeId,
                TotalPrice = order.TotalPrice,
                Status = order.Status,
                OrderDate = order.OrderDate
            };
        }

        public async Task<OrderDto> CreateOrder(CreateOrderDto createOrderDto, int userId)
        {
            var order = new Order
            {
                UserId = userId,
                CakeId = createOrderDto.CakeId,
                TotalPrice = createOrderDto.TotalPrice,
                Status = "Pending",
                OrderDate = DateTime.UtcNow,
                DeliveryDate = createOrderDto.DeliveryDate,
                DeliveryAddress = createOrderDto.DeliveryAddress ?? string.Empty,
                Notes = createOrderDto.Notes ?? string.Empty
            };

            await _orderRepository.Create(order);

            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                CakeId = order.CakeId,
                TotalPrice = order.TotalPrice,
                Status = order.Status,
                OrderDate = order.OrderDate
            };
        }

        public async Task<List<OrderDto>> GetAllAsync()
        {
            var orders = await _orderRepository.GetAll();
            return orders.Select(o => new OrderDto
            {
                Id = o.Id,
                UserId = o.UserId,
                CakeId = o.CakeId,
                TotalPrice = o.TotalPrice,
                Status = o.Status,
                OrderDate = o.OrderDate
            }).ToList();
        }

        public async Task<List<OrderDto>> GetUserOrders(int userId)
        {
            var orders = await _orderRepository.GetByUserId(userId);
            return orders.Select(o => new OrderDto
            {
                Id = o.Id,
                UserId = o.UserId,
                CakeId = o.CakeId,
                TotalPrice = o.TotalPrice,
                Status = o.Status,
                OrderDate = o.OrderDate
            }).ToList();
        }

        public async Task<OrderDto?> GetByIdAsync(int id)
        {
            var order = await _orderRepository.GetById(id);
            if (order == null) return null;

            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                CakeId = order.CakeId,
                TotalPrice = order.TotalPrice,
                Status = order.Status,
                OrderDate = order.OrderDate
            };
        }

        public async Task<OrderDto?> GetOrderById(int id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<object> GetOrderTrackingInfo(int orderId)
        {
            var order = await _orderRepository.GetById(orderId);
            if (order == null) return null!;

            return new
            {
                OrderId = order.Id,
                Status = order.Status,
                OrderDate = order.OrderDate,
                DeliveryDate = order.DeliveryDate,
                TrackingSteps = GetTrackingSteps(order.Status)
            };
        }

        public async Task UpdateAsync(int id, CreateOrderDto dto)
        {
            var order = await _orderRepository.GetById(id);
            if (order != null)
            {
                order.CakeId = dto.CakeId;
                order.TotalPrice = dto.TotalPrice;
                order.DeliveryDate = dto.DeliveryDate;
                order.DeliveryAddress = dto.DeliveryAddress ?? string.Empty;
                order.Notes = dto.Notes ?? string.Empty;

                await _orderRepository.Update(id, order);
            }
        }

        public async Task<bool> UpdateOrderStatus(int orderId, string status)
        {
            var order = await _orderRepository.GetById(orderId);
            if (order == null) return false;

            order.Status = status;
            await _orderRepository.Update(orderId, order);

            // 🔥 Bildirim Entegrasyonu
            await SendOrderStatusNotification(order.UserId, orderId, status);
            
            return true;
        }

        private async Task SendOrderStatusNotification(int userId, int orderId, string status)
        {
            string message = status switch
            {
                "Confirmed" => "Siparişiniz onaylandı! Hazırlık aşamasına geçti.",
                "InProgress" => "Siparişiniz hazırlanıyor! 🍰",
                "Ready" => "Siparişiniz hazırlandı! Teslimat için bekliyor.",
                "Delivered" => "Siparişiniz teslim edildi! Afiyet olsun! 🎉",
                _ => $"Sipariş durumunuz güncellendi: {status}"
            };

            // Database bildirimi
            await _notificationService.CreateNotification(userId, "ORDER_STATUS", message);
            
            // Push notification
            await _fcmService.SendToUser(userId, "Pastella - Sipariş Güncellemesi", message, 
                new Dictionary<string, string> 
                { 
                    { "type", "order_status" }, 
                    { "orderId", orderId.ToString() } 
                });
        }

        public async Task<bool> CancelOrder(int orderId, int userId)
        {
            var order = await _orderRepository.GetById(orderId);
            if (order == null || order.UserId != userId) return false;

            if (order.Status == "Pending" || order.Status == "Confirmed")
            {
                order.Status = "Cancelled";
                await _orderRepository.Update(orderId, order);
                return true;
            }

            return false;
        }

        public async Task DeleteAsync(int id)
        {
            await _orderRepository.Delete(id);
        }

        private List<object> GetTrackingSteps(string currentStatus)
        {
            var allSteps = new List<object>
            {
                new { Step = "Pending", Description = "Sipariş alındı", IsCompleted = true },
                new { Step = "Confirmed", Description = "Sipariş onaylandı", IsCompleted = false },
                new { Step = "InProgress", Description = "Hazırlanıyor", IsCompleted = false },
                new { Step = "Ready", Description = "Hazır", IsCompleted = false },
                new { Step = "Delivered", Description = "Teslim edildi", IsCompleted = false }
            };

            var statusOrder = new[] { "Pending", "Confirmed", "InProgress", "Ready", "Delivered" };
            var currentIndex = Array.IndexOf(statusOrder, currentStatus);

            for (int i = 0; i <= currentIndex && i < allSteps.Count; i++)
            {
                ((dynamic)allSteps[i]).IsCompleted = true;
            }

            return allSteps;
        }
    }
}