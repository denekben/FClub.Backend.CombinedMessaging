using MediatR;
using Notifications.Domain.DTOs;

namespace Notifications.Application.UseCases.Notifications.Queries
{
    public sealed record GetNotifications : IRequest<List<NotificationDto>?>;
}
