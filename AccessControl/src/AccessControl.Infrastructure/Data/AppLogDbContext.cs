using AccessControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Infrastructure.Data
{
    public class AppLogDbContext : DbContext
    {
        public DbSet<UserLog> UserLogs { get; set; }

        public AppLogDbContext(DbContextOptions<AppLogDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("FClub.AccessControl");

            var configuration = new AccessControlConfiguration();

            modelBuilder.ApplyConfiguration<UserLog>(configuration);

            base.OnModelCreating(modelBuilder);
        }
    }
}
