using AccessControl.Domain.Entities;

namespace AccessControl.Domain.Repositories
{
    public interface ITariffRepository
    {
        Task<Tariff?> GetAsync(Guid id, TariffIncludes includes);
        Task AddAsync(Tariff tariff);
        Task DeleteAsync(Guid id);
    }

    public enum TariffIncludes
    {
        ServicesTariffs = 1
    }
}
