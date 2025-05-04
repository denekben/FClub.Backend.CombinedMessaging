using MediatR;

namespace Management.Application.UseCases.Clients.Commands
{
    public sealed record DeleteClient(Guid clientId) : IRequest<Unit>;
}
