using Management.Domain.Entities;

namespace Management.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<bool> ExistsByEmailAsync(string email);
        Task<AppUser?> GetUserByEmailAsync(string email, UserIncludes includes);
        Task AddAsync(AppUser user);
        Task<AppUser?> GetAsync(Guid id);
        Task<AppUser?> GetAsync(Guid id, UserIncludes includes);
    }

    [Flags]
    public enum UserIncludes
    {
        Role = 1
    }
}
