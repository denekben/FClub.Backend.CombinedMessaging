using Microsoft.EntityFrameworkCore;
using Notifications.Domain.Entities;

namespace Notifications.Infrastructure.Data
{
    public class AppLogDbContext : DbContext
    {
        public DbSet<UserLog> UserLogs { get; set; }

        public AppLogDbContext(DbContextOptions<AppLogDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var configuration = new NotificationsConfiguration();

            modelBuilder.ApplyConfiguration<UserLog>(configuration);

            base.OnModelCreating(modelBuilder);
        }
    }
}
