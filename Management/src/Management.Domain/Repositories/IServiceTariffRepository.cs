using Management.Domain.Entities.Pivots;

namespace Management.Domain.Repositories
{
    public interface IServiceTariffRepository
    {
        Task DeleteByTariffId(Guid id);
        Task AddAsync(ServiceTariff st);
    }
}
