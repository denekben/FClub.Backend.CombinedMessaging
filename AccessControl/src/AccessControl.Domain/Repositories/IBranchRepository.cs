using AccessControll.Domain.Entities;

namespace AccessControl.Domain.Repositories
{
    public interface IBranchRepository
    {
        Task UpdateAsync(Branch branch);
        Task<Branch?> GetAsync(Guid id, BranchIncludes includes);
        Task AddAsync(Branch branch);
        Task DeleteAsync(Guid id);
    }

    [Flags]
    public enum BranchIncludes
    {
        ServicesBranches = 1
    }
}
