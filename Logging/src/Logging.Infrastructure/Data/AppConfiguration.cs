using Logging.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logging.Infrastructure.Data
{
    public class AppConfiguration : IEntityTypeConfiguration<AppUser>, IEntityTypeConfiguration<UserLog>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(au => au.Id);

            builder.HasData(Seed.AppUsers);

            builder.ToTable("AppUsers");
        }

        public void Configure(EntityTypeBuilder<UserLog> builder)
        {
            builder.HasKey(ul => ul.Id);

            builder.ToTable("UserLogs");
        }
    }
}
