namespace Notifications.Shared.DTOs
{
    public sealed record NotificationSettingsDto
    (
        bool AllowAttendanceNotifications,
        uint AttendanceNotificationPeriod,
        Guid? AttendanceNotificationId,
        bool AllowTariffNotifications,
        Guid? TariffNotificationId,
        bool AllowBranchfNotifications,
        Guid? BranchNotificationId
    );
}
