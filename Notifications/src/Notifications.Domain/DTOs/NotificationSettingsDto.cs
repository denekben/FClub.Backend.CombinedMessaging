namespace Notifications.Domain.DTOs
{
    public sealed record NotificationSettingsDto
    (
        bool AllowAttendanceNotifications,
        uint AttendanceNotificationPeriod,
        uint AttendanceNotificationReSendPeriod,
        NotificationDto? AttendanceNotification,
        bool AllowTariffNotifications,
        NotificationDto? TariffNotification,
        bool AllowBranchfNotifications,
        NotificationDto? BranchNotification
    );
}