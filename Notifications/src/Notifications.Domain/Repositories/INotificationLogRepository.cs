using Notifications.Domain.Entities;

namespace Notifications.Domain.Repositories
{
    public interface INotificationLogRepository
    {
        Task AddAsync(NotificationLog log);
    }
}
