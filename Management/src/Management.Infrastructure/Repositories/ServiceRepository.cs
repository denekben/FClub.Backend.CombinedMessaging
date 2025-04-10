using Management.Domain.Repositories;
using Management.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly AppDbContext _context;

        public ServiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Domain.Entities.Service service)
        {
            await _context.Services.AddAsync(service);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Services.Where(s => s.Id == id).ExecuteDeleteAsync();
        }

        public async Task<Domain.Entities.Service?> GetAsync(Guid id)
        {
            return await _context.Services.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Domain.Entities.Service?> GetByNameAsync(string name)
        {
            return await _context.Services.FirstOrDefaultAsync(s => s.Name == name);
        }
    }
}
