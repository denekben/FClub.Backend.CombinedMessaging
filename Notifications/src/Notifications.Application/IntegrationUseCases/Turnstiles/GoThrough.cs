using MediatR;

namespace Notifications.Application.IntegrationUseCases.Turnstiles.Commands
{
    public sealed record GoThrough(Guid ClientId) : IRequest;
}