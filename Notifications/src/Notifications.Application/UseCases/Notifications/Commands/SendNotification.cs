using MediatR;

namespace Notifications.Application.UseCases.Notifications.Commands
{
    public sealed record SendNotification(string Title, string Text) : IRequest;
}
