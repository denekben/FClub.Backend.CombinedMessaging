using Management.Domain.Entities;
using Management.Domain.Repositories;
using Management.Infrastructure.Data;

namespace Management.Infrastructure.Repositories
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
