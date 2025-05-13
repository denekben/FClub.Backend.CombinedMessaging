using Logging.Domain.Entities;

namespace Logging.Domain.Repositories
{
    public interface IUserLogRepository
    {
        Task AddAsync(UserLog log);
    }
}
