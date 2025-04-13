namespace Notifications.Domain.DTOs
{
    public sealed record NotificationSettingsDto
    (
        bool AllowAttendanceNotifications,
        uint AttendanceNotificationPeriod,
        uint AttendanceNotificationReSendPeriod,
        string AttendanceEmailSubject,
        NotificationDto? AttendanceNotification,
        bool AllowTariffNotifications,
        string TariffEmailSubject,
        NotificationDto? TariffNotification,
        bool AllowBranchfNotifications,
        string BranchEmailSubject,
        NotificationDto? BranchNotification
    );
}