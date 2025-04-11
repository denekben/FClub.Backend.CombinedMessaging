using Microsoft.EntityFrameworkCore;
using Notifications.Domain.Entities;
using Notifications.Domain.Repositories;
using Notifications.Infrastructure.Data;

namespace Notifications.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _context;

        public NotificationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Notifications.Where(n => n.Id == id).ExecuteDeleteAsync();
        }

        public async Task<Notification?> GetAsync(Guid id)
        {
            return await _context.Notifications.FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<Notification?> GetAttendanceNotificationAsync()
        {
            return await _context.NotificationSettings.AsNoTracking()
                .Join(
                    _context.Notifications,
                    ns => ns.AttendanceNotificationId,
                    n => n.Id,
                    (ns, n) => n)
                .FirstOrDefaultAsync();
        }

        public async Task<Notification?> GetBranchNotificationAsync()
        {
            return await _context.NotificationSettings.AsNoTracking()
                .Join(
                    _context.Notifications,
                    ns => ns.BranchNotificationId,
                    n => n.Id,
                    (ns, n) => n)
                .FirstOrDefaultAsync();
        }

        public async Task<Notification?> GetTariffNotificationAsync()
        {
            return await _context.NotificationSettings.AsNoTracking()
                .Join(
                    _context.Notifications,
                    ns => ns.TariffNotificationId,
                    n => n.Id,
                    (ns, n) => n)
                .FirstOrDefaultAsync();
        }
    }
}
