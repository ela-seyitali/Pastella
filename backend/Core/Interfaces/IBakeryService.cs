using Pastella.Backend.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pastella.Backend.Application.Services
{
    public interface IBakeryService
    {
        Task<BakeryDto?> CreateBakery(CreateBakeryDto createBakeryDto);
        Task<IEnumerable<BakeryDto>> GetAllBakeries();
        Task<BakeryDto?> GetBakeryById(int id);
        Task<IEnumerable<OrderDto>> GetBakeryOrders(int bakeryId);
        Task<bool> ApproveBakery(int id, bool approve);
        Task<IEnumerable<BakeryDto>> GetPendingBakeries();
    }
}