using Notifications.Domain.Entities;
using Notifications.Domain.Repositories;

namespace Notifications.Infrastructure.Repositories
{
    public class NotificationSettingsRepository : INotificationSettingsRepository
    {
        public Task<NotificationSettings> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(NotificationSettings notificationSettings)
        {
            throw new NotImplementedException();
        }
    }
}
