using AccessControl.Domain.Entities;

namespace AccessControl.Domain.Repositories
{
    public interface ITariffRepository
    {
        Task<Tariff?> GetAsync(Guid id, TariffIncludes includes);
        Task<Tariff?> GetAsync(Guid id);
        Task AddAsync(Tariff tariff);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }

    public enum TariffIncludes
    {
        ServicesTariffs = 1
    }
}
