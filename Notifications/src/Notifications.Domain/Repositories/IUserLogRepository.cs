using Notifications.Domain.Entities;

namespace Notifications.Domain.Repositories
{
    public interface IUserLogRepository
    {
        Task AddAsync(UserLog log);
    }
}
