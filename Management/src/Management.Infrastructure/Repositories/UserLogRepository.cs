using Management.Domain.Entities;
using Management.Domain.Repositories;
using Management.Infrastructure.Data;

namespace Management.Infrastructure.Repositories
{
    public class UserLogRepository : IUserLogRepository
    {
        private readonly AppDbContext _context;

        public UserLogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UserLog log)
        {
            await _context.UserLogs.AddAsync(log);
        }
    }
}
