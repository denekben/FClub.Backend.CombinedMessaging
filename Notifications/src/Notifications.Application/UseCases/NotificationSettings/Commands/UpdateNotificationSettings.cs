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
        bool AllowTariffNotifications,
        string TariffEmailSubject,
        Guid? TariffNotificationId,
        bool AllowBranchfNotifications,
        string BranchEmailSubject,
        Guid? BranchNotificationId
    ) : IRequest<NotificationSettingsDto?>;
}