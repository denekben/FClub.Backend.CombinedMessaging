using Microsoft.EntityFrameworkCore;
using Notifications.Domain.Entities;
using Notifications.Domain.Repositories;
using Notifications.Infrastructure.Data;

namespace Notifications.Infrastructure.Repositories
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

        public async Task<List<string>?> GetEmails(int limit = 100)
        {
            return await _context.Clients.Where(c => c.AllowNotifications).Take(limit).Select(c => c.Email).ToListAsync();
        }
    }
}
