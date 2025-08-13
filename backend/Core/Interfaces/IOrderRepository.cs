using Pastella.Backend.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pastella.Backend.Core.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetById(int id);
        Task<IEnumerable<Order>> GetAll();
        Task<IEnumerable<Order>> GetByUserId(int userId);
        Task Create(Order order);
        Task Update(int id, Order order);
        Task Delete(int id);
    }
}