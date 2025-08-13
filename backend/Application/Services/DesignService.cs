using Pastella.Backend.Core.DTOs;
using Pastella.Backend.Core.Entities;
using Pastella.Backend.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pastella.Backend.Application.Services
{
    public class DesignService : IDesignService
    {
        private readonly IDesignRepository _designRepository;
        private readonly IUserRepository _userRepository;

        public DesignService(IDesignRepository designRepository, IUserRepository userRepository)
        {
            _designRepository = designRepository;
            _userRepository = userRepository;
        }

        public async Task<SweetDesign> CreateDesign(DesignCreateDto designDto, int userId)
        {
            var design = new SweetDesign
            {
                Name = designDto.Name,
                Description = designDto.Description,
                Shape = designDto.Shape,
                Layers = designDto.Layers,
                ColorHex = designDto.ColorHex,
                Price = designDto.Price,
                CreatedByUserId = userId  // UserId yerine CreatedByUserId kullan
            };

            await _designRepository.Create(design);
            return design;
        }

        public async Task<SweetDesign?> GetDesign(int id)
        {
            return await _designRepository.GetById(id);
        }

        public async Task<List<DesignCreateDto>> GetAllDesignsAsync()
        {
            var designs = await _designRepository.GetAll();
            return designs.Select(d => new DesignCreateDto
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                Shape = d.Shape,
                Layers = d.Layers,
                ColorHex = d.ColorHex,
                Price = d.Price
            }).ToList();
        }

        public async Task<DesignCreateDto?> GetDesignByIdAsync(int id)
        {
            var design = await _designRepository.GetById(id);
            if (design == null) return null;

            return new DesignCreateDto
            {
                Id = design.Id,
                Name = design.Name,
                Description = design.Description,
                Shape = design.Shape,
                Layers = design.Layers,
                ColorHex = design.ColorHex,
                Price = design.Price
            };
        }

        public async Task UpdateDesignAsync(int id, DesignCreateDto dto)
        {
            var design = await _designRepository.GetById(id);
            if (design != null)
            {
                design.Name = dto.Name;
                design.Description = dto.Description;
                design.Shape = dto.Shape;
                design.Layers = dto.Layers;
                design.ColorHex = dto.ColorHex;
                design.Price = dto.Price;

                await _designRepository.Update(id, design);
            }
        }

        public async Task DeleteDesignAsync(int id)
        {
            await _designRepository.Delete(id);
        }
    }
}