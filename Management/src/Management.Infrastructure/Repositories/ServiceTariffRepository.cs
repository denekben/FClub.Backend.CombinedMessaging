using Management.Domain.Entities.Pivots;
using Management.Domain.Repositories;
using Management.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure.Repositories
{
    public class ServiceTariffRepository : IServiceTariffRepository
    {
        private readonly AppDbContext _context;

        public ServiceTariffRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ServiceTariff st)
        {
            await _context.AddAsync(st);
        }

        public async Task DeleteByTariffId(Guid id)
        {
            await _context.ServiceTariffs.Where(st => st.TariffId == id).ExecuteDeleteAsync();
        }
    }
}
