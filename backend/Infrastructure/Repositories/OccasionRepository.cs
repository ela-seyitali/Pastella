using Microsoft.EntityFrameworkCore;
using Pastella.Backend.Core.Entities;
using Pastella.Backend.Core.Interfaces;
using Pastella.Backend.Infrastructure.Data;

namespace Pastella.Backend.Infrastructure.Repositories
{
    public class OccasionRepository : IRepository<Occasion>
    {
        private readonly ApplicationDbContext _context;

        public OccasionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Occasion?> GetById(int id)
        {
            return await _context.Occasions.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Occasion>> GetAll()
        {
            return await _context.Occasions
                .Where(o => o.IsActive)
                .ToListAsync();
        }

        public async Task Create(Occasion entity)
        {
            await _context.Occasions.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, Occasion entity)
        {
            var existingOccasion = await _context.Occasions.FindAsync(id);
            if (existingOccasion != null)
            {
                existingOccasion.Name = entity.Name;
                existingOccasion.Description = entity.Description;
                existingOccasion.IconUrl = entity.IconUrl;
                existingOccasion.SuggestedDecorations = entity.SuggestedDecorations;
                existingOccasion.SuggestedColors = entity.SuggestedColors;
                existingOccasion.IsActive = entity.IsActive;

                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var occasion = await _context.Occasions.FindAsync(id);
            if (occasion != null)
            {
                occasion.IsActive = false; // Soft delete
                await _context.SaveChangesAsync();
            }
        }
    }
}