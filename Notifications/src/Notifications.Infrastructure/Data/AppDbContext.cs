using Microsoft.EntityFrameworkCore;
using Notifications.Domain.Entities;

namespace Notifications.Infrastructure.Data
{
    public sealed class AppDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationSettings> NotificationSettings { get; set; }
        public DbSet<AppUser> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("FClub.Notifications");

            var configuration = new NotificationsConfiguration();

            modelBuilder.ApplyConfiguration<Client>(configuration);
            modelBuilder.ApplyConfiguration<Notification>(configuration);
            modelBuilder.ApplyConfiguration<NotificationSettings>(configuration);
            modelBuilder.ApplyConfiguration<AppUser>(configuration);

            base.OnModelCreating(modelBuilder);
        }
    }
}
