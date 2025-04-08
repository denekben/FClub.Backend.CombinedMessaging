using Management.Domain.Entities;
using Management.Domain.Repositories;

namespace Management.Infrastructure.Repositories
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

        public Task<bool> ExistsByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Client?> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Client?> GetAsync(Guid id, ClientIncludes includes)
        {
            throw new NotImplementedException();
        }
        public Task UpdateAsync(Client client)
        {
            throw new NotImplementedException();
        }
    }
}
