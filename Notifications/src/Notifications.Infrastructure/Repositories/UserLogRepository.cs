using Notifications.Domain.Entities;
using Notifications.Domain.Repositories;
using Notifications.Infrastructure.Data;

namespace Notifications.Infrastructure.Repositories
{
    public class UserLogRepository : IUserLogRepository
    {
        private readonly AppLogDbContext _context;

        public UserLogRepository(AppLogDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UserLog log)
        {
            await _context.UserLogs.AddAsync(log);
        }
    }
}
