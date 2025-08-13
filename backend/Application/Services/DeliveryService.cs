using Pastella.Backend.Core.DTOs;
using Pastella.Backend.Core.Entities;
using Pastella.Backend.Core.Interfaces;

namespace Pastella.Backend.Application.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IRepository<DeliveryAddress> _deliveryAddressRepository;

        public DeliveryService(IRepository<DeliveryAddress> deliveryAddressRepository)
        {
            _deliveryAddressRepository = deliveryAddressRepository;
        }

        public async Task<DeliveryAddressDto?> AddDeliveryAddress(DeliveryAddressDto addressDto, int userId)
        {
            var deliveryAddress = new DeliveryAddress
            {
                Title = addressDto.Title,
                FullAddress = addressDto.FullAddress,
                City = addressDto.City,
                District = addressDto.District,
                PostalCode = addressDto.PostalCode,
                ContactPhone = addressDto.ContactPhone,
                DeliveryNotes = addressDto.DeliveryNotes,
                IsDefault = addressDto.IsDefault,
                UserId = userId
            };

            await _deliveryAddressRepository.Create(deliveryAddress);

            return new DeliveryAddressDto
            {
                Id = deliveryAddress.Id,
                Title = deliveryAddress.Title,
                FullAddress = deliveryAddress.FullAddress,
                City = deliveryAddress.City,
                District = deliveryAddress.District,
                PostalCode = deliveryAddress.PostalCode,
                ContactPhone = deliveryAddress.ContactPhone,
                DeliveryNotes = deliveryAddress.DeliveryNotes,
                IsDefault = deliveryAddress.IsDefault
            };
        }

        public async Task<IEnumerable<DeliveryAddressDto>> GetUserAddresses(int userId)
        {
            var addresses = await _deliveryAddressRepository.GetAll();
            var userAddresses = addresses.Where(a => a.UserId == userId);

            return userAddresses.Select(a => new DeliveryAddressDto
            {
                Id = a.Id,
                Title = a.Title,
                FullAddress = a.FullAddress,
                City = a.City,
                District = a.District,
                PostalCode = a.PostalCode,
                ContactPhone = a.ContactPhone,
                DeliveryNotes = a.DeliveryNotes,
                IsDefault = a.IsDefault
            });
        }

        public async Task<bool> SetDefaultAddress(int addressId, int userId)
        {
            var addresses = await _deliveryAddressRepository.GetAll();
            var userAddresses = addresses.Where(a => a.UserId == userId).ToList();

            // Reset all addresses to non-default
            foreach (var address in userAddresses)
            {
                address.IsDefault = false;
                await _deliveryAddressRepository.Update(address.Id, address);
            }

            // Set the selected address as default
            var targetAddress = userAddresses.FirstOrDefault(a => a.Id == addressId);
            if (targetAddress == null) return false;

            targetAddress.IsDefault = true;
            await _deliveryAddressRepository.Update(addressId, targetAddress);
            return true;
        }

        public async Task<IEnumerable<string>> GetAvailableTimeSlots(DateTime date)
        {
            // Available delivery time slots
            var timeSlots = new List<string>
            {
                "09:00 - 11:00",
                "11:00 - 13:00",
                "13:00 - 15:00",
                "15:00 - 17:00",
                "17:00 - 19:00",
                "19:00 - 21:00"
            };

            // In a real application, you would check existing deliveries for the date
            // and remove unavailable slots
            return await Task.FromResult(timeSlots);
        }
    }
}