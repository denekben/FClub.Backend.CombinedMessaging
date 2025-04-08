using AccessControl.Domain.Entities;
using AccessControl.Domain.Repositories;

namespace AccessControl.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        public Task AddAsync(Client client)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Client> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Client> GetAsync(Guid id, ClientIncludes includes)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Client client)
        {
            throw new NotImplementedException();
        }
    }
}
