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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var result = await _notificationService.DeleteNotificationAsync(id);
            if (!result)
            {
                return NotFound(new { Message = "Notification not found" });
            }

            // Return a 200 OK response with a message
            return Ok(new { Message = "Notification deleted successfully", NotificationId = id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotification(int id, [FromBody] Notification updatedNotification)
        {
            var result = await _notificationService.UpdateNotificationAsync(id, updatedNotification);
            if (!result)
            {
                return NotFound(new { Message = "Notification not found" });
            }

            // Return a 200 OK response with a success message and the updated notification
            return Ok(new { Message = "Notification updated successfully", UpdatedNotification = updatedNotification });
        }
    }
}