using MediatR;
using Notifications.Shared.DTOs;

namespace Notifications.Application.UseCases.NotificationSettings.Commands
{
    public sealed record UpdateNotificationSettings(
        Guid Id,
        bool AllowAttendanceNotifications,
        uint AttendanceNotificationPeriod,
        Guid? AttendanceNotificationId,
        bool AllowTariffNotifications,
        Guid? TariffNotificationId,
        bool AllowBranchfNotifications,
        Guid? BranchNotificationId
    ) : IRequest<NotificationSettingsDto?>;
}