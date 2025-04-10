using AccessControl.Domain.Repositories;
using AccessControl.Infrastructure.Data;
using AccessControll.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Infrastructure.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly AppDbContext _context;

        public ServiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Service service)
        {
            await _context.Services.AddAsync(service);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Services.Where(s => s.Id == id).ExecuteDeleteAsync();
        }

        public async Task<Service?> GetAsync(Guid id)
        {
            return await _context.Services.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Service>?> GetByBranchId(Guid id)
        {
            return await _context.ServiceBranches
                .Where(sb => sb.BranchId == id)
                .Join(
                    _context.Services,
                    sb => sb.ServiceId,
                    s => s.Id,
                    (sb, s) => s)
                .ToListAsync();
        }

        public async Task<List<Service>?> GetByTariffId(Guid id)
        {
            return await _context.ServiceTariffs
                .Where(st => st.TariffId == id)
                .Join(
                    _context.Services,
                    st => st.ServiceId,
                    s => s.Id,
                    (st, s) => s)
                .ToListAsync();
        }
    }
}