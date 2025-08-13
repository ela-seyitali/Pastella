using Pastella.Backend.Core.Entities;
using Pastella.Backend.Core.Interfaces;
using Pastella.Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pastella.Backend.Infrastructure.Repositories
{
    public class DecorationRepository : IRepository<Decoration>
    {
        private readonly ApplicationDbContext _context;

        public DecorationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Decoration entity)
        {
            await _context.Decorations.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var decoration = await _context.Decorations.FindAsync(id);
            if (decoration != null)
            {
                _context.Decorations.Remove(decoration);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Decoration>> GetAll()
        {
            return await _context.Decorations.ToListAsync();
        }

        public async Task<Decoration?> GetById(int id)
        {
            return await _context.Decorations.FindAsync(id);
        }

        public async Task Update(int id, Decoration entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
} 