using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notifications.Domain.Entities;

namespace Notifications.Infrastructure.Data
{
    public sealed class NotificationsConfiguration :
        IEntityTypeConfiguration<Client>, IEntityTypeConfiguration<Notification>,
        IEntityTypeConfiguration<NotificationSettings>,
        IEntityTypeConfiguration<UserLog>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("Clients");
        }

        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("Notifications");
        }

        public void Configure(EntityTypeBuilder<NotificationSettings> builder)
        {
            builder.HasKey(c => c.Id);

            builder
                .HasOne(ns => ns.AttendanceNotification)
                .WithOne(n => n.NotificationSettings)
                .HasForeignKey<NotificationSettings>(ns => ns.AttendanceNotificationId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasOne(ns => ns.TariffNotification)
                .WithOne(n => n.NotificationSettings)
                .HasForeignKey<NotificationSettings>(ns => ns.TariffNotificationId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasOne(ns => ns.BranchNotification)
                .WithOne(n => n.NotificationSettings)
                .HasForeignKey<NotificationSettings>(ns => ns.BranchNotificationId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.ToTable("NotificationSettings");
        }

        public void Configure(EntityTypeBuilder<UserLog> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("UserLogs");
        }
    }
}
