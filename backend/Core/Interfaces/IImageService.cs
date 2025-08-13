using Pastella.Backend.Core.DTOs;
using Pastella.Backend.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Pastella.Backend.Application.Services
{
    public interface IImageService
    {
        Task<DesignImageDto> CreateAsync(DesignImageDto dto);
        Task<List<DesignImageDto>> GetAllAsync();
        Task<DesignImageDto?> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task<string> UploadImageAsync(IFormFile image);
        // Ek olarak resim yükleme (örneğin blob storage'a) metotları eklenebilir.
    }
}