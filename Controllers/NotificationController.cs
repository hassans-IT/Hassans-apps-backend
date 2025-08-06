using Microsoft.AspNetCore.Mvc;
using NotificationBackend.Models;
using NotificationBackend.Services;

namespace NotificationBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService _notificationService;

        public NotificationController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetNotifications()
        {
            var notifications = await _notificationService.GetAllNotificationsAsync();
            return Ok(notifications);
        }

        [HttpPost]
        public async Task<IActionResult> AddNotification([FromBody] Notification notification)
        {
            await _notificationService.AddNotificationAsync(notification);
            return CreatedAtAction(nameof(GetNotifications), new { }, notification);
        }
    }
}