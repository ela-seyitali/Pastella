using Microsoft.EntityFrameworkCore;
using Pastella.Backend.Core.Entities;
using Pastella.Backend.Core.Interfaces;
using Pastella.Backend.Infrastructure.Data;

namespace Pastella.Backend.Infrastructure.Repositories
{
    public class CommentRepository : IRepository<Comment>
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Comment?> GetById(int id)
        {
            return await _context.Comments
                .Include(c => c.User)
                .Include(c => c.Cake)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Comment>> GetAll()
        {
            return await _context.Comments
                .Include(c => c.User)
                .Include(c => c.Cake)
                .ToListAsync();
        }

        public async Task Create(Comment entity)
        {
            await _context.Comments.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, Comment entity)
        {
            var existingComment = await _context.Comments.FindAsync(id);
            if (existingComment != null)
            {
                existingComment.Rating = entity.Rating;
                existingComment.Message = entity.Message;

                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Comment>> GetCakeComments(int cakeId)
        {
            return await _context.Comments
                .Include(c => c.User)
                .Where(c => c.CakeId == cakeId)
                .OrderByDescending(c => c.CreatedDate)
                .ToListAsync();
        }
    }
}