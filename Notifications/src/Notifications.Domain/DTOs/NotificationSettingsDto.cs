namespace Notifications.Domain.DTOs
{
    public sealed record NotificationSettingsDto
    (
        bool AllowAttendanceNotifications,
        uint AttendanceNotificationPeriod,
        NotificationDto? AttendanceNotification,
        bool AllowTariffNotifications,
        NotificationDto? TariffNotification,
        bool AllowBranchfNotifications,
        NotificationDto? BranchNotification
    );
}