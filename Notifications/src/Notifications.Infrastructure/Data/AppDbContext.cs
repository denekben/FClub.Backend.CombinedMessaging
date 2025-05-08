using Microsoft.EntityFrameworkCore;
using Notifications.Domain.Entities;

namespace Notifications.Infrastructure.Data
{
    public sealed class AppDbContext : DbContext
    {
        private readonly Seed _seeder;

        public DbSet<Client> Clients { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationSettings> NotificationSettings { get; set; }
        public DbSet<AppUser> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options, Seed seeder) : base(options)
        {
            _seeder = seeder;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var configuration = new NotificationsConfiguration();

            modelBuilder.ApplyConfiguration<Client>(configuration);
            modelBuilder.ApplyConfiguration<Notification>(configuration);
            modelBuilder.ApplyConfiguration<NotificationSettings>(configuration);
            modelBuilder.ApplyConfiguration<AppUser>(configuration);

            _seeder.SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
