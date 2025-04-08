using MediatR;

namespace Notifications.Application.UseCases.Turnstiles.Commands
{
    public sealed record GoThrough(Guid ClientId) : IRequest;
}