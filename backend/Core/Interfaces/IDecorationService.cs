using Pastella.Backend.Core.DTOs;
using Pastella.Backend.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pastella.Backend.Application.Services
{
    public interface IDecorationService
    {
        Task<DecorationDto> CreateAsync(AddDecorationDto dto);
        Task<List<DecorationDto>> GetAllAsync();
        Task<DecorationDto?> GetByIdAsync(int id);
        Task UpdateAsync(int id, AddDecorationDto dto);
        Task DeleteAsync(int id);
    }
} 