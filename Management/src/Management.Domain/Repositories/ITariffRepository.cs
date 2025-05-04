using Management.Domain.Entities;

namespace Management.Domain.Repositories
{
    public interface ITariffRepository
    {
        Task<Tariff?> GetAsync(Guid id);
        Task<Tariff?> GetAsync(Guid id, TariffIncludes includes);
        Task AddAsync(Tariff tariff);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<List<Tariff>?> GetAllAsync();
        void UpdateAsync(Tariff tariff);
    }

    [Flags]
    public enum TariffIncludes
    {
        ServiceTariffs = 1,
        Services = 2
    }
}
