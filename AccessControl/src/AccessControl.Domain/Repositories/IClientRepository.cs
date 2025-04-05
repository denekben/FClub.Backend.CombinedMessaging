using AccessControl.Domain.Entities;

namespace AccessControl.Domain.Repositories
{
    public interface IClientRepository
    {
        Task<Client> GetAsync(Guid id, ClientIncludes includes);
    }

    [Flags]
    public enum ClientIncludes
    {
        Membership = 1,
        Tariff = 2,
        ServiceTariff = 4
    }
}
