using Microsoft.AspNetCore.Mvc;
using NotificationBackend.Services;

namespace NotificationBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Returns JWT token if valid, otherwise null
            var token = _userService.ValidateUserAndGenerateToken(request.Username, request.Password);

            if (token == null)
                return Unauthorized(new { message = "Invalid username or password" });

            // Return token to frontend
            return Ok(new { token });
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            var isRegistered = _userService.RegisterUser(request.Username, request.Password);

            if (!isRegistered)
                return BadRequest(new { message = "Username already exists" });

            return Ok(new { message = "Registration successful" });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
