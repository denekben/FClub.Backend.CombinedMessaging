using MediatR;

namespace Notifications.Application.UseCases.Notifications.Commands
{
    public sealed record SendNotification(Guid NotificationId) : IRequest;
}
