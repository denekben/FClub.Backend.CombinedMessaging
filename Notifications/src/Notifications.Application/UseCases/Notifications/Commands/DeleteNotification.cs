using MediatR;

namespace Notifications.Application.UseCases.Notifications.Commands
{
    public sealed record DeleteNotification(Guid notificationId) : IRequest;
}
