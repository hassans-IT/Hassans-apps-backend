using Microsoft.EntityFrameworkCore;
using NotificationBackend.Models;

namespace NotificationBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
