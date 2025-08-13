using Pastella.Backend.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pastella.Backend.Application.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationDto>> GetUserNotifications(int userId);
        Task<bool> MarkAsRead(int notificationId);
        Task CreateNotification(int userId, string type, string message);
        
        // 🆕 Yeni metodlar
        Task SendPromotionNotification(string title, string message, List<int>? userIds = null);
        Task SendBirthdayReminder(int userId, string customerName, DateTime birthdayDate);
        Task SendDeliveryNotification(int userId, int orderId, string deliveryStatus);
    }
}