using Management.Domain.Entities;
using Management.Domain.Repositories;
using Management.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure.Repositories
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

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Tariffs.AnyAsync(t => t.Id == id);
        }

        public async Task<List<Tariff>?> GetAllAsync()
        {
            return await _context.Tariffs.ToListAsync();
        }

        public async Task<Tariff?> GetAsync(Guid id)
        {
            return await _context.Tariffs.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Tariff?> GetAsync(Guid id, TariffIncludes includes)
        {
            var query = _context.Tariffs.Where(t => t.Id == id);

            if (includes.HasFlag(TariffIncludes.ServiceTariffs))
                query = query.Include(t => t.ServiceTariffs);
            if (includes.HasFlag(TariffIncludes.Services))
                query = query
                    .Include(t => t.ServiceTariffs)
                    .ThenInclude(st => st.Service);

            return await query.FirstOrDefaultAsync();
        }

        public void UpdateAsync(Tariff tariff)
        {
            _context.Tariffs.Update(tariff);
        }
    }
}
