using Pastella.Backend.Core.DTOs;
using Pastella.Backend.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pastella.Backend.Application.Services
{
    public interface IOrderService
    {
        Task<OrderDto> CreateAsync(CreateOrderDto dto);
        Task<OrderDto> CreateOrder(CreateOrderDto createOrderDto, int userId);
        Task<List<OrderDto>> GetAllAsync();
        Task<List<OrderDto>> GetUserOrders(int userId);
        Task<OrderDto?> GetByIdAsync(int id);
        Task<OrderDto?> GetOrderById(int id);
        Task<object> GetOrderTrackingInfo(int orderId);
        Task UpdateAsync(int id, CreateOrderDto dto);
        Task<bool> UpdateOrderStatus(int orderId, string status);
        Task<bool> CancelOrder(int orderId, int userId);
        Task DeleteAsync(int id);
    }
}