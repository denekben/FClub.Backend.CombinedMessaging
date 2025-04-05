using Management.Domain.Entities;
using Management.Domain.Repositories;

namespace Management.Infrastructure.Repositories
{
    public class SocialGroupRepository : ISocialGroupRepository
    {
        public Task AddAsync(SocialGroup socialGroup)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<SocialGroup?> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(SocialGroup socialGroup)
        {
            throw new NotImplementedException();
        }
    }
}
