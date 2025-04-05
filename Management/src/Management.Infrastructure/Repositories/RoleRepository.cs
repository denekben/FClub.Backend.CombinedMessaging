using Management.Domain.Entities;
using Management.Domain.Repositories;

namespace Management.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        public Task<Role?> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Role?> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
