using Management.Domain.Entities;

namespace Management.Domain.Repositories
{
    public interface IServiceRepository
    {
        Task<Service?> GetAsync(Guid id);
        Task<Service?> GetByNameAsync(string name);
        Task<Service?> GetByNameNoTrackingAsync(string name);
        Task DeleteAsync(Guid id);
        Task AddAsync(Service service);
    }
}
