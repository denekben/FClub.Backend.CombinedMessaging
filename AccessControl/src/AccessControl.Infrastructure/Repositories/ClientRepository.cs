using AccessControl.Domain.Entities;
using AccessControl.Domain.Repositories;
using AccessControl.Infrastructure.Data;
using FClub.Backend.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Client client)
        {
            await _context.Clients.AddAsync(client);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Clients.Where(c => c.Id == id).ExecuteDeleteAsync();
        }

        public async Task<Client?> GetAsync(Guid id)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<Client?> GetAsync(Guid id, ClientIncludes includes)
        {
            var query = _context.Clients.Where(c => c.Id == id);

            query = query.Include(c => c.Membership);

            if (query.Select(c => c.Membership) != null)
            {
                if (includes.HasFlag(ClientIncludes.ServiceTariff))
                {
                    query = query.Include(c => c.Membership)
                                    .ThenInclude(m => m.Tariff)
                                    .ThenInclude(t => t.ServiceTariffs);
                }
                else if (includes.HasFlag(ClientIncludes.Tariff))
                {
                    query = query.Include(c => c.Membership)
                                    .ThenInclude(m => m.Tariff);
                }
            }
            else
            {
                if (includes.HasFlag(ClientIncludes.ServiceTariff))
                    throw new BadRequestException("Cannot include ServiceTariff: Membership is null");
                if (includes.HasFlag(ClientIncludes.Tariff))
                    throw new BadRequestException("Cannot include Tariff: Membership is null");
            }

            return query.FirstOrDefaultAsync();
        }
    }
}
