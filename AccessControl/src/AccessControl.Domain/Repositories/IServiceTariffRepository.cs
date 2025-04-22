using AccessControl.Domain.Entities.Pivots;

namespace AccessControl.Domain.Repositories
{
    public interface IServiceTariffRepository
    {
        Task AddAsync(ServiceTariff st);
        Task DeleteByTariffId(Guid id);
    }
}
