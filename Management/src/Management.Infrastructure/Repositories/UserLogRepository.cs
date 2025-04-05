using Management.Domain.Entities;
using Management.Domain.Repositories;

namespace Management.Infrastructure.Repositories
{
    public class UserLogRepository : IUserLogRepository
    {
        public Task AddAsync(UserLog log)
        {
            throw new NotImplementedException();
        }
    }
}
