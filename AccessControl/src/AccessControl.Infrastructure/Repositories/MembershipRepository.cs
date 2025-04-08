using AccessControl.Domain.Entities;
using AccessControl.Domain.Repositories;

namespace AccessControl.Infrastructure.Repositories
{
    public class MembershipRepository : IMembershipRepository
    {
        public Task AddAsync(Membership membership)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Membership?> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Membership membership)
        {
            throw new NotImplementedException();
        }
    }
}
