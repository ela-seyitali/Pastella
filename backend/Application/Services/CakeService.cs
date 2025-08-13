using Pastella.Backend.Core.DTOs;
using Pastella.Backend.Core.Entities;
using Pastella.Backend.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pastella.Backend.Application.Services
{
    public class CakeService : ICakeService
    {
        private readonly ICakeRepository _cakeRepository;

        public CakeService(ICakeRepository cakeRepository)
        {
            _cakeRepository = cakeRepository;
        }

        public async Task<CakeDto> CreateAsync(CreateCakeDto dto)
        {
            var cake = new Cake
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl,
                SweetDesignId = dto.SweetDesignId
            };
            await _cakeRepository.Create(cake);
            // TODO: AutoMapper eklendiğinde burada Cake'ten CakeDto'ya dönüşüm yapılacak.
            return new CakeDto // Geçici olarak doğrudan atama yapılıyor
            {
                Id = cake.Id,
                Name = cake.Name,
                Description = cake.Description,
                Price = cake.Price,
                ImageUrl = cake.ImageUrl,
                SweetDesignId = cake.SweetDesignId
            };
        }

        public async Task<List<CakeDto>> GetAllAsync()
        {
            var cakes = await _cakeRepository.GetAll();
            // TODO: AutoMapper eklendiğinde burada IEnumerable<Cake>'ten List<CakeDto>'ya dönüşüm yapılacak.
            var cakeDtos = new List<CakeDto>();
            foreach (var cake in cakes)
            {
                cakeDtos.Add(new CakeDto
                {
                    Id = cake.Id,
                    Name = cake.Name,
                    Description = cake.Description,
                    Price = cake.Price,
                    ImageUrl = cake.ImageUrl,
                    SweetDesignId = cake.SweetDesignId
                });
            }
            return cakeDtos;
        }

        public async Task<CakeDto?> GetByIdAsync(int id)
        {
            var cake = await _cakeRepository.GetById(id);
            if (cake == null)
            {
                return null;
            }
            // TODO: AutoMapper eklendiğinde burada Cake'ten CakeDto'ya dönüşüm yapılacak.
            return new CakeDto
            {
                Id = cake.Id,
                Name = cake.Name,
                Description = cake.Description,
                Price = cake.Price,
                ImageUrl = cake.ImageUrl,
                SweetDesignId = cake.SweetDesignId
            };
        }

        public async Task UpdateAsync(int id, CreateCakeDto dto)
        {
            var existingCake = await _cakeRepository.GetById(id);
            if (existingCake == null)
            {
                throw new Exception("Cake not found");
            }

            existingCake.Name = dto.Name;
            existingCake.Description = dto.Description;
            existingCake.Price = dto.Price;
            existingCake.ImageUrl = dto.ImageUrl;
            existingCake.SweetDesignId = dto.SweetDesignId;

            await _cakeRepository.Update(id, existingCake);
        }

        public async Task DeleteAsync(int id)
        {
            await _cakeRepository.Delete(id);
        }
    }
} 