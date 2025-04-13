using AccessControl.Domain.Entities;

namespace AccessControl.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<AppUser?> GetAsync(Guid id);
        Task AddAsync(AppUser user);
        Task<bool?> IsBlocked(Guid id);
    }
}
