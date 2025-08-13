using Pastella.Backend.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pastella.Backend.Application.Services
{
    public interface IDeliveryService
    {
        Task<DeliveryAddressDto?> AddDeliveryAddress(DeliveryAddressDto addressDto, int userId);
        Task<IEnumerable<DeliveryAddressDto>> GetUserAddresses(int userId);
        Task<bool> SetDefaultAddress(int addressId, int userId);
        Task<IEnumerable<string>> GetAvailableTimeSlots(DateTime date);
    }
}