using Notifications.Domain.Entities;
using Notifications.Domain.Repositories;
using Notifications.Infrastructure.Data;

namespace Notifications.Infrastructure.Repositories
{
    public class NotificationLogRepository : INotificationLogRepository
    {
        private readonly AppDbContext _context;

        public NotificationLogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(NotificationLog log)
        {
            await _context.NotificationLogs.AddAsync(log);
        }
    }
}
