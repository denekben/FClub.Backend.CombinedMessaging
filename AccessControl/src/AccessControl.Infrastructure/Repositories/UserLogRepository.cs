using AccessControl.Domain.Entities;
using AccessControl.Domain.Repositories;
using AccessControl.Infrastructure.Data;

namespace AccessControl.Infrastructure.Repositories
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
