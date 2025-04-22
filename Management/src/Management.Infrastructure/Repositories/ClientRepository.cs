using Management.Domain.Entities;
using Management.Domain.Repositories;
using Management.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure.Repositories
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

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Clients.AnyAsync(c => c.Email == email);
        }

        public async Task<Client?> GetAsync(Guid id)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Client?> GetAsync(Guid id, ClientIncludes includes)
        {
            var query = _context.Clients.Where(c => c.Id == id);

            if (includes.HasFlag(ClientIncludes.SocialGroup))
                query = query.Include(c => c.SocialGroup);
            if (includes.HasFlag(ClientIncludes.Membership))
                query = query.Include(c => c.Membership);

            return await query.FirstOrDefaultAsync();
        }
    }
}
