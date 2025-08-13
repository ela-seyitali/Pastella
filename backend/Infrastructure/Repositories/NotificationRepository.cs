using Microsoft.EntityFrameworkCore;
using Pastella.Backend.Core.Entities;
using Pastella.Backend.Core.Interfaces;
using Pastella.Backend.Infrastructure.Data;

namespace Pastella.Backend.Infrastructure.Repositories
{
    public class NotificationRepository : IRepository<Notification>
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Notification?> GetById(int id)
        {
            return await _context.Notifications
                .Include(n => n.User)
                .FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<IEnumerable<Notification>> GetAll()
        {
            return await _context.Notifications
                .Include(n => n.User)
                .ToListAsync();
        }

        public async Task Create(Notification entity)
        {
            await _context.Notifications.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, Notification entity)
        {
            var existingNotification = await _context.Notifications.FindAsync(id);
            if (existingNotification != null)
            {
                existingNotification.Type = entity.Type;
                existingNotification.Message = entity.Message;
                existingNotification.IsRead = entity.IsRead;

                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification != null)
            {
                _context.Notifications.Remove(notification);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Notification>> GetUserNotifications(int userId)
        {
            return await _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedDate)
                .ToListAsync();
        }
    }
}