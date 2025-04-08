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
                attendanceNotification?.AsDto(),
                settings.AllowTariffNotifications,
                tariffNotification?.AsDto(),
                settings.AllowBranchfNotifications,
                branchNotification?.AsDto()
            );

        }

        public static NotificationLogDto AsDto(this NotificationLog log)
        {
            return new(
                log.Id,
                log.NotificationId,
                log.NotificationTitle,
                log.NotificationText
            );
        }

        public static UserLogDto AsDto(this UserLog log)
        {
            return new(
                log.Id,
                log.AppUserId,
                log.ServiceName,
                log.Text,
                log.CreatedDate,
                log.UpdatedDate
            );
        }
    }
}