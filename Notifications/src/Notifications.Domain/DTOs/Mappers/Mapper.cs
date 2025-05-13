using Notifications.Domain.Entities;

namespace Notifications.Domain.DTOs.Mappers
{
    public static class Mapper
    {
        public static NotificationDto AsDto(this Notification notification)
        {
            return new(
                notification.Id,
                notification.Title,
                notification.Text,
                notification.CreatedDate,
                notification.UpdatedDate
            );
        }

        public static NotificationSettingsDto AsDto(
            this NotificationSettings settings,
            Notification? attendanceNotification,
            Notification? tariffNotification,
            Notification? branchNotification)
        {
            return new(
                settings.AllowAttendanceNotifications,
                settings.AttendanceNotificationPeriod,
                settings.AttendanceNotificationReSendPeriod,
                settings.AttendanceEmailSubject,
                attendanceNotification?.AsDto(),
                settings.TariffEmailSubject,
                tariffNotification?.AsDto(),
                settings.BranchEmailSubject,
                branchNotification?.AsDto()
            );
        }
    }
}