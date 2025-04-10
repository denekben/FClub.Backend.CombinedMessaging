using AccessControl.Domain.Entities;
using AccessControl.Domain.Repositories;
using AccessControl.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Infrastructure.Repositories
{
    public class TariffRepository : ITariffRepository
    {
        private readonly AppDbContext _context;

        public TariffRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Tariff tariff)
        {
            await _context.Tariffs.AddAsync(tariff);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Tariffs.Where(t => t.Id == id).ExecuteDeleteAsync();
        }

        public async Task<Tariff?> GetAsync(Guid id, TariffIncludes includes)
        {
            var query = _context.Tariffs.Where(t => t.Id == id);

            if (includes.HasFlag(TariffIncludes.ServicesTariffs))
                query = query.Include(t => t.ServiceTariffs);

            return await query.FirstOrDefaultAsync();
        }
    }
}
