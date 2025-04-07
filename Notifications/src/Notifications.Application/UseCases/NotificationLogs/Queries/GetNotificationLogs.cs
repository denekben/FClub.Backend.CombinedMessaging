using MediatR;
using Notifications.Domain.DTOs;

namespace Notifications.Application.UseCases.NotificationLogs.Queries
{
    public sealed record GetNotificationLogs : IRequest<List<NotificationLogDto>?>;
}
