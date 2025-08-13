using Pastella.Backend.Core.Entities;
using Pastella.Backend.Core.Interfaces;
using Pastella.Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pastella.Backend.Infrastructure.Repositories
{
    public class DesignImageRepository : IDesignImageRepository
    {
        private readonly ApplicationDbContext _context;

        public DesignImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(DesignImage entity)
        {
            await _context.DesignImages.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var designImage = await _context.DesignImages.FindAsync(id);
            if (designImage != null)
            {
                _context.DesignImages.Remove(designImage);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<DesignImage>> GetAll()
        {
            return await _context.DesignImages.ToListAsync();
        }

        public async Task<DesignImage?> GetById(int id)
        {
            return await _context.DesignImages.FindAsync(id);
        }

        public async Task Update(int id, DesignImage entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
} 