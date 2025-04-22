using AccessControl.Domain.Entities.Pivots;
using AccessControl.Domain.Repositories;
using AccessControl.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Infrastructure.Repositories
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
            await _context.ServiceTariffs.AddAsync(st);
        }

        public async Task DeleteByTariffId(Guid id)
        {
            await _context.ServiceTariffs.Where(st => st.TariffId == id).ExecuteDeleteAsync();
        }
    }
}
