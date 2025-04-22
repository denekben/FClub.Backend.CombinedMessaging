using AccessControl.Domain.Entities;

namespace AccessControl.Domain.Repositories
{
    public interface IBranchRepository
    {
        Task<Branch?> GetAsync(Guid id);
        Task<Branch?> GetAsync(Guid id, BranchIncludes includes);
        Task AddAsync(Branch branch);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }

    [Flags]
    public enum BranchIncludes
    {
        ServicesBranches = 1,
        Services = 2
    }
}
