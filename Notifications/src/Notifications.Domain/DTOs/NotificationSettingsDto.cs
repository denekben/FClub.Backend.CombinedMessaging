namespace Notifications.Domain.DTOs
{
    public sealed record NotificationSettingsDto
    (
        bool AllowAttendanceNotifications,
        uint AttendanceNotificationPeriod,
        uint AttendanceNotificationReSendPeriod,
        string AttendanceEmailSubject,
        NotificationDto? AttendanceNotification,
        string TariffEmailSubject,
        NotificationDto? TariffNotification,
        string BranchEmailSubject,
        NotificationDto? BranchNotification
    );
}