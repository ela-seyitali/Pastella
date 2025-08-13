using Microsoft.EntityFrameworkCore;
using Pastella.Backend.Core.Entities;
using Pastella.Backend.Core.Interfaces;
using Pastella.Backend.Infrastructure.Data;

namespace Pastella.Backend.Infrastructure.Repositories
{
    public class DeviceTokenRepository : IRepository<DeviceToken>
    {
        private readonly ApplicationDbContext _context;

        public DeviceTokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DeviceToken?> GetById(int id)
        {
            return await _context.DeviceTokens
                .Include(dt => dt.User)
                .FirstOrDefaultAsync(dt => dt.Id == id);
        }

        public async Task<IEnumerable<DeviceToken>> GetAll()
        {
            return await _context.DeviceTokens
                .Include(dt => dt.User)
                .ToListAsync();
        }

        public async Task Create(DeviceToken entity)
        {
            await _context.DeviceTokens.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, DeviceToken entity)
        {
            var existingToken = await _context.DeviceTokens.FindAsync(id);
            if (existingToken != null)
            {
                existingToken.Token = entity.Token;
                existingToken.DeviceType = entity.DeviceType;
                existingToken.LastUsed = entity.LastUsed;
                existingToken.IsActive = entity.IsActive;
                existingToken.UserId = entity.UserId;

                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var token = await _context.DeviceTokens.FindAsync(id);
            if (token != null)
            {
                _context.DeviceTokens.Remove(token);
                await _context.SaveChangesAsync();
            }
        }
    }
}