using Notifications.Domain.Entities;
using Notifications.Domain.Repositories;

namespace Notifications.Infrastructure.Repositories
{
    public class UserLogRepository : IUserLogRepository
    {
        public Task AddAsync(UserLog log)
        {
            throw new NotImplementedException();
        }
    }
}
