using Pastella.Backend.Core.Entities;
using Pastella.Backend.Core.Interfaces;
using Pastella.Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pastella.Backend.Infrastructure.Repositories
{
    public class BakeryRepository : IRepository<Bakery>
    {
        private readonly ApplicationDbContext _context;

        public BakeryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Bakery?> GetById(int id)
        {
            return await _context.Bakeries.FindAsync(id);
        }

        public async Task<IEnumerable<Bakery>> GetAll()
        {
            return await _context.Bakeries.ToListAsync();
        }

        public async Task Create(Bakery entity)
        {
            await _context.Bakeries.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, Bakery entity)
        {
            var existingBakery = await GetById(id);
            if (existingBakery != null)
            {
                existingBakery.Name = entity.Name;
                existingBakery.Description = entity.Description;
                existingBakery.Address = entity.Address;
                existingBakery.Phone = entity.Phone;
                existingBakery.Email = entity.Email;
                existingBakery.IsApproved = entity.IsApproved;
                existingBakery.SocialMediaLinks = entity.SocialMediaLinks;
                
                _context.Bakeries.Update(existingBakery);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var bakery = await GetById(id);
            if (bakery != null)
            {
                _context.Bakeries.Remove(bakery);
                await _context.SaveChangesAsync();
            }
        }

        // Additional methods specific to Bakery
        public async Task<IEnumerable<Bakery>> GetPendingBakeries()
        {
            return await _context.Bakeries
                .Where(b => !b.IsApproved)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetBakeryOrders(int bakeryId)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Cake)
                .Where(o => o.BakeryId == bakeryId)
                .ToListAsync();
        }
    }
}