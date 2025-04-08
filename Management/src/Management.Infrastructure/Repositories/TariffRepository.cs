using Management.Domain.Entities;
using Management.Domain.Repositories;

namespace Management.Infrastructure.Repositories
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

        public Task<Tariff?> GetAsync(Guid id)
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
