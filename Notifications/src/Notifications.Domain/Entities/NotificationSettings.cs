using FClub.Backend.Common.Exceptions;

namespace Notifications.Domain.Entities
{
    public sealed class NotificationSettings
    {
        public Guid Id { get; init; }

        public bool AllowAttendanceNotifications { get; set; }
        public uint AttendanceNotificationPeriod { get; set; }
        public uint AttendanceNotificationReSendPeriod { get; set; }
        public Guid? AttendanceNotificationId { get; set; }
        public Notification? AttendanceNotification { get; set; }

        public bool AllowTariffNotifications { get; set; }
        public Guid? TariffNotificationId { get; set; }
        public Notification? TariffNotification { get; set; }

        public bool AllowBranchfNotifications { get; set; }
        public Guid? BranchNotificationId { get; set; }
        public Notification? BranchNotification { get; set; }

        private NotificationSettings() { }

        private NotificationSettings(
            bool allowAttendanceNotifications, uint attendanceNotificationPeriod,
            uint attendanceNotificationReSendPeriod, Guid? attendanceNotificationId,
            bool allowTariffNotifications, Guid? tariffNotificationId,
            bool allowBranchfNotifications, Guid? branchNotificationId)
        {
            Id = Guid.NewGuid();

            AllowAttendanceNotifications = allowAttendanceNotifications;
            AttendanceNotificationPeriod = attendanceNotificationPeriod;
            AttendanceNotificationReSendPeriod = attendanceNotificationReSendPeriod;
            AttendanceNotificationId = attendanceNotificationId;

            AllowTariffNotifications = allowTariffNotifications;
            TariffNotificationId = tariffNotificationId;

            AllowBranchfNotifications = allowBranchfNotifications;
            BranchNotificationId = branchNotificationId;
        }

        public static NotificationSettings Create(
            bool allowAttendanceNotifications, uint attendanceNotificationPeriod,
            uint attendanceNotificationReSendPeriod, Guid? attendanceNotificationId,
            bool allowTariffNotifications, Guid? tariffNotificationId,
            bool allowBranchfNotifications, Guid? branchNotificationId)
        {
            if (allowAttendanceNotifications && attendanceNotificationId == null)
                throw new DomainException($"Invalid value for NotificationSettings[attendanceNotificationId]. Entered value {attendanceNotificationId}");
            if (allowTariffNotifications && tariffNotificationId == null)
                throw new DomainException($"Invalid value for NotificationSettings[tariffNotificationId]. Entered value {tariffNotificationId}");
            if (allowBranchfNotifications && branchNotificationId == null)
                throw new DomainException($"Invalid value for NotificationSettings[branchNotificationId]. Entered value {branchNotificationId}");
            if (attendanceNotificationPeriod <= 0)
                throw new DomainException($"Invalid value for NotificationSettings[attendanceNotificationPeriod]. Entered value {attendanceNotificationPeriod}");
            if (attendanceNotificationReSendPeriod <= 0)
                throw new DomainException($"Invalid value for NotificationSettings[attendanceNotificationPeriod]. Entered value {attendanceNotificationReSendPeriod}");

            return new NotificationSettings(
                allowAttendanceNotifications, attendanceNotificationPeriod,
                attendanceNotificationReSendPeriod, attendanceNotificationId,
                allowTariffNotifications, tariffNotificationId,
                allowBranchfNotifications, branchNotificationId);
        }

        public void UpdateDetails(
            bool allowAttendanceNotifications, uint attendanceNotificationPeriod,
            uint attendanceNotificationReSendPeriod, Guid? attendanceNotificationId,
            bool allowTariffNotifications, Guid? tariffNotificationId,
            bool allowBranchfNotifications, Guid? branchNotificationId)
        {
            if (allowAttendanceNotifications && attendanceNotificationId == null)
                throw new DomainException($"Invalid value for NotificationSettings[attendanceNotificationId]. Entered value {attendanceNotificationId}");
            if (allowTariffNotifications && tariffNotificationId == null)
                throw new DomainException($"Invalid value for NotificationSettings[tariffNotificationId]. Entered value {tariffNotificationId}");
            if (allowBranchfNotifications && branchNotificationId == null)
                throw new DomainException($"Invalid value for NotificationSettings[branchNotificationId]. Entered value {branchNotificationId}");
            if (attendanceNotificationPeriod <= 0)
                throw new DomainException($"Invalid value for NotificationSettings[attendanceNotificationPeriod]. Entered value {attendanceNotificationPeriod}");
            if (attendanceNotificationReSendPeriod <= 0)
                throw new DomainException($"Invalid value for NotificationSettings[attendanceNotificationPeriod]. Entered value {attendanceNotificationReSendPeriod}");


            AllowAttendanceNotifications = allowAttendanceNotifications;
            AttendanceNotificationPeriod = attendanceNotificationPeriod;
            AttendanceNotificationReSendPeriod = attendanceNotificationReSendPeriod;
            AttendanceNotificationId = attendanceNotificationId;

            AllowTariffNotifications = allowTariffNotifications;
            TariffNotificationId = tariffNotificationId;

            AllowBranchfNotifications = allowBranchfNotifications;
            BranchNotificationId = branchNotificationId;
        }
    }
}
