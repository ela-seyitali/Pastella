using Pastella.Backend.Core.DTOs;
using System.Threading.Tasks;

namespace Pastella.Backend.Application.Services
{
    public interface ICustomCakeService
    {
        Task<OrderDto?> CreateCustomCake(CreateCustomCakeDto createCustomCakeDto, int userId);
        Task<decimal> CalculatePrice(string size, string shape, int layers, bool hasPhotoCake, int decorationCount);
        Task<object> GetCustomizationOptions();
    }
}