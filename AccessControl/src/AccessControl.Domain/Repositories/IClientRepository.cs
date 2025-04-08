using AccessControl.Domain.Entities;

namespace AccessControl.Domain.Repositories
{
    public interface IClientRepository
    {
        Task UpdateAsync(Client client);
        Task<Client> GetAsync(Guid id);
        Task<Client> GetAsync(Guid id, ClientIncludes includes);
        Task AddAsync(Client client);
        Task DeleteAsync(Guid id);
    }

    [Flags]
    public enum ClientIncludes
    {
        Membership = 1,
        ServiceTariff = 2,
        Tariff = 4
    }
}
