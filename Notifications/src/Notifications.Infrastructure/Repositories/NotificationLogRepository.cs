using Notifications.Domain.Entities;
using Notifications.Domain.Repositories;

namespace Notifications.Infrastructure.Repositories
{
    public class NotificationLogRepository : INotificationLogRepository
    {
        public Task AddAsync(NotificationLog log)
        {
            throw new NotImplementedException();
        }
    }
}
