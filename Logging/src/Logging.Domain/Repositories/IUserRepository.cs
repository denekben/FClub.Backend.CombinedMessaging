using Logging.Domain.Entities;

namespace Logging.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<AppUser?> GetAsync(Guid id);
        Task<bool?> IsBlockedAsync(Guid id);
        Task AddAsync(AppUser user);
    }
}
