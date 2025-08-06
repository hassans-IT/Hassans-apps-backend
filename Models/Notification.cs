using System;

namespace NotificationBackend.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime StartDate { get; set; } // When the notification becomes active
        public DateTime EndDate { get; set; }   // When the notification expires
        public DateTime CreatedAt { get; set; } // When the notification was created
    }
}