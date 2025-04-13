using Notifications.Domain.Entities;

namespace Notifications.Domain.Repositories
{
    public interface IClientRepository
    {
        Task<Client?> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task AddAsync(Client client);
        Task<List<string>?> GetEmailsAsync(int limit = 100);
    }
}
