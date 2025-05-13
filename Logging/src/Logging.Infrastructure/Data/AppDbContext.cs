using Logging.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logging.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<UserLog> UserLogs { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var configuration = new AppConfiguration();

            modelBuilder.ApplyConfiguration<AppUser>(configuration);
            modelBuilder.ApplyConfiguration<UserLog>(configuration);

            base.OnModelCreating(modelBuilder);
        }
    }
}
