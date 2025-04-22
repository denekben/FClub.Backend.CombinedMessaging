using Management.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure.Data
{
    public class AppLogDbContext : DbContext
    {
        public DbSet<UserLog> UserLogs { get; set; }

        public AppLogDbContext(DbContextOptions<AppLogDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("FClub.Management");

            var configuration = new ManagementConfiguration();

            modelBuilder.ApplyConfiguration<UserLog>(configuration);

            base.OnModelCreating(modelBuilder);
        }
    }
}
