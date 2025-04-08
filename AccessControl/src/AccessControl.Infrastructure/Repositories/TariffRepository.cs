using AccessControl.Domain.Entities;
using AccessControl.Domain.Repositories;

namespace AccessControl.Infrastructure.Repositories
{
    public class TariffRepository : ITariffRepository
    {
        public Task AddAsync(Tariff tariff)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Tariff?> GetAsync(Guid id, TariffIncludes includes)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Tariff tariff)
        {
            throw new NotImplementedException();
        }
    }
}
