using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.SignalR;
using NotificationBackend.Hubs;
using NotificationBackend.Models;

namespace NotificationBackend.Services
{
    public class NotificationService
    {
        private readonly List<Notification> _notifications = new();
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public IEnumerable<Notification> GetAllNotifications()
        {
            return _notifications;
        }

       public void AddNotification(Notification notification)
{
    notification.Id = _notifications.Count > 0 ? _notifications.Max(n => n.Id) + 1 : 1;
    notification.CreatedAt = DateTime.Now; // Set the creation timestamp
    _notifications.Add(notification);

    // Send the notification to all connected clients
    _hubContext.Clients.All.SendAsync("ReceiveNotification", notification);
}
    }
}