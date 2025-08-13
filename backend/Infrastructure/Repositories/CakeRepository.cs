using Pastella.Backend.Core.Entities;
using Pastella.Backend.Core.Interfaces;
using Pastella.Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pastella.Backend.Infrastructure.Repositories
{
    public class CakeRepository : ICakeRepository
    {
        private readonly ApplicationDbContext _context;

        public CakeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Cake entity)
        {
            await _context.Cakes.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var cake = await _context.Cakes.FindAsync(id);
            if (cake != null)
            {
                _context.Cakes.Remove(cake);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Cake>> GetAll()
        {
            return await _context.Cakes.ToListAsync();
        }

        public async Task<Cake?> GetById(int id)
        {
            return await _context.Cakes.FindAsync(id);
        }

        public async Task Update(int id, Cake entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
} 