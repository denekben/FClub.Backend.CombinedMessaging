using Management.Domain.DTOs;
using MediatR;

namespace Management.Application.UseCases.Clients.Queries
{
    public sealed record GetClient(Guid ClientId) : IRequest<ClientDto?>;
}
