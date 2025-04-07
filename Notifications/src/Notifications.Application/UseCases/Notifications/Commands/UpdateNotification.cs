using MediatR;
using Notifications.Domain.DTOs;

namespace Notifications.Application.UseCases.Notifications.Commands
{
    public sealed record UpdateNotification(Guid Id, string Title, string Text) : IRequest<NotificationDto?>;
}
