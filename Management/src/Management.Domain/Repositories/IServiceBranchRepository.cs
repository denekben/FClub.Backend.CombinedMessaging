using Management.Domain.Entities.Pivots;

namespace Management.Domain.Repositories
{
    public interface IServiceBranchRepository
    {
        Task DeleteAsync(Guid id);
        Task DeleteByBranchId(Guid id);
        Task AddAsync(ServiceBranch sb);
    }
}
