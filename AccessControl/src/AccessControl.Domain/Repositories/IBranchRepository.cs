using AccessControll.Domain.Entities;

namespace AccessControl.Domain.Repositories
{
    public interface IBranchRepository
    {
        Task<Branch?> GetAsync(Guid id);
        Task<Branch?> GetAsync(Guid id, BranchIncludes includes);
        Task AddAsync(Branch branch);
        Task UpdateAsync(Branch branch);
        Task DeleteAsync(Guid id);
    }

    [Flags]
    public enum BranchIncludes
    {
        ServicesBranches = 1
    }
}
