using Pastella.Backend.Core.DTOs;
using Pastella.Backend.Core.Entities;
using Pastella.Backend.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pastella.Backend.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> CreateUserAsync(UserRegistrationDto dto)
        {
            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "User"
            };

            await _userRepository.Create(user);

            return new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAll();
            return users.Select(u => new UserDto
            {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.Email,
                Role = u.Role
            }).ToList();
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            return await GetAllUsersAsync();
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task UpdateUserAsync(int id, UserDto dto)
        {
            var user = await _userRepository.GetById(id);
            if (user != null)
            {
                user.FullName = dto.FullName;
                user.Email = dto.Email;
                user.Role = dto.Role;

                await _userRepository.Update(id, user);
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.Delete(id);
        }
    }
}