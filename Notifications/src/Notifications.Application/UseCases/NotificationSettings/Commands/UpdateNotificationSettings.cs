using MediatR;
using Notifications.Domain.DTOs;

namespace Notifications.Application.UseCases.NotificationSettings.Commands
{
    public sealed record UpdateNotificationSettings(
        bool AllowAttendanceNotifications,
        uint AttendanceNotificationPeriod,
        uint AttendanceNotificationReSendPeriod,
        string AttendanceEmailSubject,
        Guid? AttendanceNotificationId,
        string TariffEmailSubject,
        Guid? TariffNotificationId,
        string BranchEmailSubject,
        Guid? BranchNotificationId
    ) : IRequest<NotificationSettingsDto?>;
}