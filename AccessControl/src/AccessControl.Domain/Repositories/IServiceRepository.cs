using AccessControll.Domain.Entities;

namespace AccessControl.Domain.Repositories
{
    public interface IServiceRepository
    {
        Task<Service?> GetAsync(Guid id);
        Task AddAsync(Service service);
        Task UpdateAsync(Service service);
        Task DeleteAsync(Guid id);
    }
}
