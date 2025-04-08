using MediatR;
using Notifications.Domain.DTOs;

namespace Notifications.Application.UseCases.NotificationSettings.Commands
{
    public sealed record UpdateNotificationSettings(
        Guid Id,
        bool AllowAttendanceNotifications,
        uint AttendanceNotificationPeriod,
        uint AttendanceNotificationReSendPeriod,
        Guid? AttendanceNotificationId,
        bool AllowTariffNotifications,
        Guid? TariffNotificationId,
        bool AllowBranchfNotifications,
        Guid? BranchNotificationId
    ) : IRequest<NotificationSettingsDto?>;
}