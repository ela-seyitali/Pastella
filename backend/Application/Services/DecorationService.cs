using Pastella.Backend.Core.DTOs;
using Pastella.Backend.Core.Entities;
using Pastella.Backend.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pastella.Backend.Application.Services
{
    public class DecorationService : IDecorationService
    {
        private readonly IRepository<Decoration> _decorationRepository;

        public DecorationService(IRepository<Decoration> decorationRepository)
        {
            _decorationRepository = decorationRepository;
        }

        public async Task<DecorationDto> CreateAsync(AddDecorationDto dto)
        {
            var decoration = new Decoration
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl
            };
            await _decorationRepository.Create(decoration);

            // TODO: AutoMapper eklendiğinde burada Decoration'dan DecorationDto'ya dönüşüm yapılacak.
            return new DecorationDto
            {
                Id = decoration.Id,
                Name = decoration.Name,
                Description = decoration.Description,
                Price = decoration.Price,
                ImageUrl = decoration.ImageUrl
            };
        }

        public async Task<List<DecorationDto>> GetAllAsync()
        {
            var decorations = await _decorationRepository.GetAll();
            var decorationDtos = new List<DecorationDto>();
            foreach (var deco in decorations)
            {
                decorationDtos.Add(new DecorationDto
                {
                    Id = deco.Id,
                    Name = deco.Name,
                    Description = deco.Description,
                    Price = deco.Price,
                    ImageUrl = deco.ImageUrl
                });
            }
            return decorationDtos;
        }

        public async Task<DecorationDto?> GetByIdAsync(int id)
        {
            var decoration = await _decorationRepository.GetById(id);
            if (decoration == null)
            {
                return null;
            }
            return new DecorationDto
            {
                Id = decoration.Id,
                Name = decoration.Name,
                Description = decoration.Description,
                Price = decoration.Price,
                ImageUrl = decoration.ImageUrl
            };
        }

        public async Task UpdateAsync(int id, AddDecorationDto dto)
        {
            var existingDecoration = await _decorationRepository.GetById(id);
            if (existingDecoration == null)
            {
                throw new Exception("Decoration not found.");
            }

            existingDecoration.Name = dto.Name;
            existingDecoration.Description = dto.Description;
            existingDecoration.Price = dto.Price;
            existingDecoration.ImageUrl = dto.ImageUrl;

            await _decorationRepository.Update(id, existingDecoration);
        }

        public async Task DeleteAsync(int id)
        {
            await _decorationRepository.Delete(id);
        }
    }
} 