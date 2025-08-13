using Pastella.Backend.Core.DTOs;
using Pastella.Backend.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pastella.Backend.Application.Services
{
    public interface ICakeService
    {
        Task<CakeDto> CreateAsync(CreateCakeDto dto);
        Task<List<CakeDto>> GetAllAsync();
        Task<CakeDto?> GetByIdAsync(int id);
        Task UpdateAsync(int id, CreateCakeDto dto);
        Task DeleteAsync(int id);
    }
} 