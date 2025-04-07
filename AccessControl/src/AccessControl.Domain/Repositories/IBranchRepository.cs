using AccessControll.Domain.Entities;

namespace AccessControl.Domain.Repositories
{
    public interface IBranchRepository
    {
        Task<Branch?> GetAsync(Guid id, BranchIncludes includes);
    }

    [Flags]
    public enum BranchIncludes
    {
        ServicesBranches = 1
    }
}
