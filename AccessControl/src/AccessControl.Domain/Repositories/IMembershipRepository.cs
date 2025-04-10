using AccessControl.Domain.Entities;

namespace AccessControl.Domain.Repositories
{
    public interface IMembershipRepository
    {
        Task<Membership?> GetAsync(Guid id);
        Task AddAsync(Membership membership);
        Task DeleteAsync(Guid id);
    }
}
