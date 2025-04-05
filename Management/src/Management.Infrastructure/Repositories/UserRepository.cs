using Management.Domain.Entities;
using Management.Domain.Repositories;

namespace Management.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task AddAsync(AppUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<AppUser?> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<AppUser?> GetAsync(Guid id, UserIncludes includes)
        {
            throw new NotImplementedException();
        }

        public Task<AppUser?> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<AppUser?> GetUserByEmailAsync(string email, UserIncludes includes)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsBlockedAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(AppUser user)
        {
            throw new NotImplementedException();
        }
    }
}
