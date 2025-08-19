using Microsoft.AspNetCore.SignalR;
using NotificationBackend.Data;
using NotificationBackend.Hubs;
using NotificationBackend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NotificationBackend.Services
{
    public class NotificationService
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(AppDbContext context, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task<IEnumerable<Notification>> GetAllNotificationsAsync()
        {
            var sixMonthsAgo = DateTime.Now.AddMonths(-6); // Calculate the date 6 months ago
            return await _context.Notifications
                .Where(n => n.CreatedAt >= sixMonthsAgo) // Filter notifications created within the last 6 months
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task AddNotificationAsync(Notification notification)
        {
            notification.CreatedAt = DateTime.Now;

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            await _hubContext.Clients.All.SendAsync("ReceiveNotification", notification);
        }

        public async Task<bool> DeleteNotificationAsync(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
            {
                return false; // Notification not found
            }

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateNotificationAsync(int id, Notification updatedNotification)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
            {
                return false; // Notification not found
            }

            notification.Title = updatedNotification.Title;
            notification.Message = updatedNotification.Message;
            notification.StartDate = updatedNotification.StartDate;
            notification.EndDate = updatedNotification.EndDate;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
