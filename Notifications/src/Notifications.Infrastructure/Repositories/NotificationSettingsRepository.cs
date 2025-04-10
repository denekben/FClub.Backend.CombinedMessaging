using Microsoft.EntityFrameworkCore;
using Notifications.Domain.Entities;
using Notifications.Domain.Repositories;
using Notifications.Infrastructure.Data;

namespace Notifications.Infrastructure.Repositories
{
    public class NotificationSettingsRepository : INotificationSettingsRepository
    {
        private readonly AppDbContext _context;

        public NotificationSettingsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<NotificationSettings?> GetAsync(Guid id)
        {
            return await _context.NotificationSettings.FirstOrDefaultAsync(ns => ns.Id == id);
        }
    }
}
