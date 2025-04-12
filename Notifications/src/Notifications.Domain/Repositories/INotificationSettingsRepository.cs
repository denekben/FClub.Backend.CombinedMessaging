using Notifications.Domain.Entities;

namespace Notifications.Domain.Repositories
{
    public interface INotificationSettingsRepository
    {
        Task<NotificationSettings?> GetAsync(Guid id);
        Task<NotificationSettings?> GetAsync();
    }
}
