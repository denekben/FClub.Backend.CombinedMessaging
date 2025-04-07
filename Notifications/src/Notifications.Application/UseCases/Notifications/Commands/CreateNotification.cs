using MediatR;
using Notifications.Domain.DTOs;

namespace Notifications.Application.UseCases.Notifications.Commands
{
    public sealed record CreateNotification(string Title, string Text) : IRequest<NotificationDto?>;
}
