using Management.Domain.Entities;

namespace Management.Domain.Repositories
{
    public interface ISocialGroupRepository
    {
        Task<bool> ExistsAsync(Guid id);
        Task<SocialGroup?> GetAsync(Guid id);
        Task AddAsync(SocialGroup socialGroup);
        Task DeleteAsync(Guid id);
    }
}
