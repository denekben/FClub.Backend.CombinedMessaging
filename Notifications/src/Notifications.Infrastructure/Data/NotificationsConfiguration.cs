using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notifications.Domain.Entities;

namespace Notifications.Infrastructure.Data
{
    public sealed class NotificationsConfiguration :
        IEntityTypeConfiguration<Client>, IEntityTypeConfiguration<Notification>,
        IEntityTypeConfiguration<NotificationSettings>,
        IEntityTypeConfiguration<UserLog>, IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(c => c.Id);

            builder
                .OwnsOne(au => au.FullName, ownedBuilder =>
                {
                    ownedBuilder.Property(fn => fn.FirstName);
                    ownedBuilder.Property(fn => fn.SecondName);
                    ownedBuilder.Property(fn => fn.Patronymic);
                });

            builder.ToTable("Clients");
        }

        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("Notifications");

            builder.HasData([Seed.AttendanceNotification, Seed.TariffNotification, Seed.BranchNotification]);
        }

        public void Configure(EntityTypeBuilder<NotificationSettings> builder)
        {
            builder.HasKey(c => c.Id);

            builder
                .HasOne(ns => ns.AttendanceNotification)
                .WithOne(n => n.AttendanceNotificationSettings)
                .HasForeignKey<NotificationSettings>(ns => ns.AttendanceNotificationId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasOne(ns => ns.TariffNotification)
                .WithOne(n => n.TariffNotificationSettings)
                .HasForeignKey<NotificationSettings>(ns => ns.TariffNotificationId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasOne(ns => ns.BranchNotification)
                .WithOne(n => n.BranchNotificationSettings)
                .HasForeignKey<NotificationSettings>(ns => ns.BranchNotificationId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.ToTable("NotificationSettings");

            builder.HasData(Seed.NotificationSettings);
        }

        public void Configure(EntityTypeBuilder<UserLog> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("UserLogs");
        }

        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(u => u.Id);

            builder.ToTable("AppUsers");

            builder.HasData(Seed.AppUsers);
        }
    }
}
