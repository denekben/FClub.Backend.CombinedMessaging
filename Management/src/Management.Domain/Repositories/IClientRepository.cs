using Management.Domain.Entities;

namespace Management.Domain.Repositories
{
    public interface IClientRepository
    {
        Task AddAsync(Client client);
        Task DeleteAsync(Guid id);
        Task<Client?> GetAsync(Guid id);
        Task<Client?> GetAsync(Guid id, ClientIncludes includes);
        Task<bool> ExistsByEmailAsync(string email);
    }

    [Flags]
    public enum ClientIncludes
    {
        SocialGroup = 1,
        Membership = 2
    }
}
