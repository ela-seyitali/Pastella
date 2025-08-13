using Pastella.Backend.Core.DTOs;
using Pastella.Backend.Core.Entities;
using Pastella.Backend.Core.Interfaces;

namespace Pastella.Backend.Application.Services
{
    public class OccasionService : IOccasionService
    {
        private readonly IRepository<Occasion> _occasionRepository;

        public OccasionService(IRepository<Occasion> occasionRepository)
        {
            _occasionRepository = occasionRepository;
        }

        public async Task<IEnumerable<OccasionDto>> GetAllOccasions()
        {
            var occasions = await _occasionRepository.GetAll();
            return occasions.Select(o => new OccasionDto
            {
                Id = o.Id,
                Name = o.Name,
                Description = o.Description,
                IconUrl = o.IconUrl,
                SuggestedDecorations = o.SuggestedDecorations,
                SuggestedColors = o.SuggestedColors
            });
        }

        public async Task<OccasionDto?> GetOccasionSuggestions(int occasionId)
        {
            var occasion = await _occasionRepository.GetById(occasionId);
            if (occasion == null) return null;

            return new OccasionDto
            {
                Id = occasion.Id,
                Name = occasion.Name,
                Description = occasion.Description,
                IconUrl = occasion.IconUrl,
                SuggestedDecorations = occasion.SuggestedDecorations,
                SuggestedColors = occasion.SuggestedColors
            };
        }
    }
}