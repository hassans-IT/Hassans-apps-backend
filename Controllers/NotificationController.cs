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
        public IActionResult GetNotifications()
        {
            return Ok(_notificationService.GetAllNotifications());
        }

        [HttpPost]
        public IActionResult AddNotification([FromBody] Notification notification)
        {
            _notificationService.AddNotification(notification);
            return CreatedAtAction(nameof(GetNotifications), notification);
        }
    }
}