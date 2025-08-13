using Microsoft.EntityFrameworkCore;
using Pastella.Backend.Core.Entities;
using Pastella.Backend.Core.Interfaces;
using Pastella.Backend.Infrastructure.Data;
using System.Collections.Generic; // Add this for IEnumerable
using System.Threading.Tasks;

namespace Pastella.Backend.Infrastructure.Repositories
{
    public class DesignRepository : IDesignRepository
    {
        private readonly ApplicationDbContext _context;

        public DesignRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SweetDesign?> GetById(int id)
        {
            return await _context.SweetDesigns.FindAsync(id);
        }

        public async Task Create(SweetDesign design)
        {
            _context.SweetDesigns.Add(design);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, SweetDesign design)
        {
            _context.Entry(design).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var design = await _context.SweetDesigns.FindAsync(id);
            if (design != null)
            {
                _context.SweetDesigns.Remove(design);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<SweetDesign>> GetAll()
        {
            return await _context.SweetDesigns.ToListAsync();
        }
    }
} 