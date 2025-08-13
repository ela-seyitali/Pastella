using Pastella.Backend.Core.DTOs;
using Pastella.Backend.Core.Entities;
using Pastella.Backend.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Pastella.Backend.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly IDesignImageRepository _designImageRepository;

        public ImageService(IDesignImageRepository designImageRepository)
        {
            _designImageRepository = designImageRepository;
        }

        public async Task<DesignImageDto> CreateAsync(DesignImageDto dto)
        {
            var designImage = new DesignImage
            {
                ImageUrl = dto.ImageUrl,
                SweetDesignId = dto.SweetDesignId
            };

            await _designImageRepository.Create(designImage);

            dto.Id = designImage.Id; // Id'yi set et
            return dto;
        }

        public async Task<List<DesignImageDto>> GetAllAsync()
        {
            var designImages = await _designImageRepository.GetAll();
            var designImageDtos = new List<DesignImageDto>();
            foreach (var image in designImages)
            {
                designImageDtos.Add(new DesignImageDto
                {
                    Id = image.Id, // Id eklendi
                    ImageUrl = image.ImageUrl,
                    SweetDesignId = image.SweetDesignId
                });
            }
            return designImageDtos;
        }

        public async Task<DesignImageDto?> GetByIdAsync(int id)
        {
            var designImage = await _designImageRepository.GetById(id);
            if (designImage == null)
            {
                return null;
            }
            return new DesignImageDto
            {
                Id = designImage.Id, // Id eklendi
                ImageUrl = designImage.ImageUrl,
                SweetDesignId = designImage.SweetDesignId
            };
        }

        public async Task DeleteAsync(int id)
        {
            await _designImageRepository.Delete(id);
        }

        public async Task<string> UploadImageAsync(IFormFile image)
        {
            // For now, return a placeholder URL
            // In a real implementation, you would upload to cloud storage (Azure Blob, AWS S3, etc.)
            var fileName = $"{Guid.NewGuid()}_{image.FileName}";
            var imageUrl = $"/uploads/{fileName}";
            
            // TODO: Implement actual file upload logic here
            // For example: upload to wwwroot/uploads folder or cloud storage
            
            return await Task.FromResult(imageUrl);
        }
    }
}