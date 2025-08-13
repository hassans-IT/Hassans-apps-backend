using Microsoft.Extensions.Configuration;
using NotificationBackend.Data;
using NotificationBackend.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;

namespace NotificationBackend.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;
        private readonly string _jwtSecret;

        public UserService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _jwtSecret = configuration["JwtSettings:SecretKey"]; // Read from appsettings
        }

        public string ValidateUserAndGenerateToken(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool RegisterUser(string username, string password)
        {
            if (_context.Users.Any(u => u.Username == username))
                return false;

            var user = new User
            {
                Username = username,
                Password = password
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }
    }
}
