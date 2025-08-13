using Pastella.Backend.Core.DTOs;
using Pastella.Backend.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pastella.Backend.Application.Services
{
    public interface IUserService
    {
        Task<UserDto> CreateUserAsync(UserRegistrationDto dto);
        Task<List<UserDto>> GetAllUsersAsync();
        Task<List<UserDto>> GetAllUsers();
        Task<UserDto?> GetUserByIdAsync(int id);
        Task UpdateUserAsync(int id, UserDto dto);
        Task DeleteUserAsync(int id);
    }
}