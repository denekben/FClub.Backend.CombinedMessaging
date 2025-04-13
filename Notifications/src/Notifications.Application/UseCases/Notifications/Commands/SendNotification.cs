using MediatR;

namespace Notifications.Application.UseCases.Notifications.Commands
{
    public sealed record SendNotification(string Subject, string Title, string Text, bool SaveNotification) : IRequest;

}
