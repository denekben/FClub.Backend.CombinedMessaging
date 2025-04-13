using Notifications.Domain.Entities;

namespace Notifications.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<AppUser?> GetAsync(Guid id);
        Task AddAsync(AppUser user);
        Task<bool?> IsBlocked(Guid id);
    }
}
