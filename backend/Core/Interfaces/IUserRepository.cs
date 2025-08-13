using Pastella.Backend.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pastella.Backend.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetById(int id);
        Task<User?> GetByEmail(string email);
        Task<User?> GetByResetToken(string token);
        Task<User?> GetByRefreshToken(string refreshToken);
        Task<IEnumerable<User>> GetAll();
        Task Create(User user);
        Task Update(int id, User user);
        Task Delete(int id);
    }
}