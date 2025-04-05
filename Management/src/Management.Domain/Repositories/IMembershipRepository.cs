using Management.Domain.Entities;

namespace Management.Domain.Repositories
{
    public interface IMembershipRepository
    {
        Task AddAsync(Membership membership);
        Task<Membership?> GetAsync(Guid id);
        Task<Membership?> GetAsync(Guid id, MembershipIncludes includes);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Membership membership);
    }

    [Flags]
    public enum MembershipIncludes
    {
        Client = 1,
        Tariff = 2
    }
}
