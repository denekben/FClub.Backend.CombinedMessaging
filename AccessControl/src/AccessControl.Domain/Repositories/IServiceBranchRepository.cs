using AccessControl.Domain.Entities.Pivots;

namespace AccessControl.Domain.Repositories
{
    public interface IServiceBranchRepository
    {
        Task AddAsync(ServiceBranch sb);
        Task DeleteAsync(Guid id);
        Task DeleteByBranchId(Guid id);
    }
}
