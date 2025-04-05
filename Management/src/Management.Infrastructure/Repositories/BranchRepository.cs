using Management.Domain.Entities;
using Management.Domain.Repositories;

namespace Management.Infrastructure.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        public Task AddAsync(Branch branch)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Branch?> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Branch?> GetAsync(Guid id, BranchIncludes includes)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Branch branch)
        {
            throw new NotImplementedException();
        }
    }
}
