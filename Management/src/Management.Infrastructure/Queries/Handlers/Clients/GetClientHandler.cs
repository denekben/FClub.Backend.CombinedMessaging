using Management.Application.UseCases.Clients.Queries;
using Management.Domain.DTOs;
using Management.Domain.DTOs.Mappers;
using Management.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure.Queries.Handlers.Clients
{
    public sealed class GetClientHandler : IRequestHandler<GetClient, ClientDto?>
    {
        private readonly AppDbContext _context;

        public GetClientHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ClientDto?> Handle(GetClient query, CancellationToken cancellationToken)
        {
            var client = await _context.Clients.Where(c => c.Id == query.ClientId).Include(c => c.Membership).Include(c => c.SocialGroup).FirstOrDefaultAsync();
            return client?.AsDto(client.Membership, client.SocialGroup);
        }
    }
}
