using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pastella.Backend.Core.Interfaces
{
    public interface IRepository<T>
    {
        Task<T?> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task Create(T entity);
        Task Update(int id, T entity);
        Task Delete(int id);
    }
} 