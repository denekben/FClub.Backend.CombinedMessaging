using Management.Domain.DTOs;
using MediatR;

namespace Management.Application.UseCases.Clients.Queries
{
    public sealed record GetClients : IRequest<List<ClientDto>?>;
}
