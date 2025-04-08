using MediatR;

namespace AccessControl.Application.IntegrationUseCases.Clients
{
    public sealed record DeleteClient(Guid Id) : IRequest;
}
