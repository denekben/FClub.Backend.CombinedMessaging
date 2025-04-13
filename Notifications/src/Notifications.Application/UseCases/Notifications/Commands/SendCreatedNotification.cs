using MediatR;

namespace Notifications.Application.UseCases.Notifications.Commands
{
    public sealed record SendCreatedNotification(string Subject, Guid NotificationId) : IRequest;
}
