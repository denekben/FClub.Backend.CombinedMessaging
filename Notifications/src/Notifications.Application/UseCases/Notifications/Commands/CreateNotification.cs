using MediatR;
using Notifications.Shared.DTOs;

namespace Notifications.Application.UseCases.Notifications.Commands
{
    public sealed record CreateNotification(string Title, string Text) : IRequest<NotificationDto?>;
}
