using AccessControl.Domain.Entities;
using AccessControl.Domain.Repositories;

namespace AccessControl.Infrastructure.Repositories
{
    public class UserLogRepository : IUserLogRepository
    {
        public Task AddAsync(UserLog log)
        {
            throw new NotImplementedException();
        }
    }
}
