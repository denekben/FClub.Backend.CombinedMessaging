using AccessControl.Domain.Entities;
using AccessControl.Domain.Entities.Pivots;
using AccessControll.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<ServiceBranch> ServiceBranches { get; set; }
        public DbSet<ServiceTariff> ServiceTariffs { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<EntryLog> EntryLogs { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<StatisticNote> StatisticNotes { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }
        public DbSet<Turnstile> Turnstiles { get; set; }
        public DbSet<UserLog> UserLogs { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("FClub.AccessControl");

            var configuration = new AccessControlConfiguration();
            modelBuilder.ApplyConfiguration<ServiceBranch>(configuration);
            modelBuilder.ApplyConfiguration<ServiceTariff>(configuration);
            modelBuilder.ApplyConfiguration<Branch>(configuration);
            modelBuilder.ApplyConfiguration<Client>(configuration);
            modelBuilder.ApplyConfiguration<EntryLog>(configuration);
            modelBuilder.ApplyConfiguration<Membership>(configuration);
            modelBuilder.ApplyConfiguration<Service>(configuration);
            modelBuilder.ApplyConfiguration<StatisticNote>(configuration);
            modelBuilder.ApplyConfiguration<Tariff>(configuration);
            modelBuilder.ApplyConfiguration<Turnstile>(configuration);
            modelBuilder.ApplyConfiguration<UserLog>(configuration);

            base.OnModelCreating(modelBuilder);
        }
    }
}
