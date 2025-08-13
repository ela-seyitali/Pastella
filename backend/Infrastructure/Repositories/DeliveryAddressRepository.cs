using Microsoft.EntityFrameworkCore;
using Pastella.Backend.Core.Entities;
using Pastella.Backend.Core.Interfaces;
using Pastella.Backend.Infrastructure.Data;

namespace Pastella.Backend.Infrastructure.Repositories
{
    public class DeliveryAddressRepository : IRepository<DeliveryAddress>
    {
        private readonly ApplicationDbContext _context;

        public DeliveryAddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DeliveryAddress?> GetById(int id)
        {
            return await _context.DeliveryAddresses
                .Include(d => d.User)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<DeliveryAddress>> GetAll()
        {
            return await _context.DeliveryAddresses
                .Include(d => d.User)
                .ToListAsync();
        }

        public async Task Create(DeliveryAddress entity)
        {
            await _context.DeliveryAddresses.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, DeliveryAddress entity)
        {
            var existingAddress = await _context.DeliveryAddresses.FindAsync(id);
            if (existingAddress != null)
            {
                existingAddress.Title = entity.Title;
                existingAddress.FullAddress = entity.FullAddress;
                existingAddress.City = entity.City;
                existingAddress.District = entity.District;
                existingAddress.PostalCode = entity.PostalCode;
                existingAddress.ContactPhone = entity.ContactPhone;
                existingAddress.DeliveryNotes = entity.DeliveryNotes;
                existingAddress.IsDefault = entity.IsDefault;

                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var address = await _context.DeliveryAddresses.FindAsync(id);
            if (address != null)
            {
                _context.DeliveryAddresses.Remove(address);
                await _context.SaveChangesAsync();
            }
        }
    }
}