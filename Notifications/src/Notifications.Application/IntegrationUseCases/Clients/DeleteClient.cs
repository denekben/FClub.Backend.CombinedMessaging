using MediatR;

namespace Notifications.Application.IntegrationUseCases.Clients
{
    public sealed record DeleteClient(Guid Id) : IRequest;
}
