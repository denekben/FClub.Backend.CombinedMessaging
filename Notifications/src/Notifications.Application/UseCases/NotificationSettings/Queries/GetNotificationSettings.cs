using MediatR;
using Notifications.Domain.DTOs;

namespace Notifications.Application.UseCases.NotificationSettings.Queries
{
    public sealed record GetNotificationSettings : IRequest<NotificationSettingsDto?>;
}
