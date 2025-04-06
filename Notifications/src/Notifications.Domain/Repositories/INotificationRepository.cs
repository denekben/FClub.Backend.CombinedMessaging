using Notifications.Domain.Entities;

namespace Notifications.Domain.Repositories
{
    public interface INotificationRepository
    {
        Task<Notification?> GetAsync(Guid id);
        Task AddAsync(Notification notification);
        Task UpdateAsync(Notification notification);
        Task DeleteAsync(Guid id);
    }
}
