using Pastella.Backend.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pastella.Backend.Application.Services
{
    public interface IOccasionService
    {
        Task<IEnumerable<OccasionDto>> GetAllOccasions();
        Task<OccasionDto?> GetOccasionSuggestions(int occasionId);
    }
}