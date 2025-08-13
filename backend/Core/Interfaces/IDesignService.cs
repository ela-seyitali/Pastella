using Pastella.Backend.Core.DTOs;
using Pastella.Backend.Core.Entities;
using System.Threading.Tasks;

namespace Pastella.Backend.Application.Services
{
    public interface IDesignService
    {
        Task<SweetDesign> CreateDesign(DesignCreateDto designDto, int userId);
        Task<SweetDesign?> GetDesign(int id);
    }
} 