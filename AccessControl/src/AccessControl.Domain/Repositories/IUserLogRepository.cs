using AccessControl.Domain.Entities;

namespace AccessControl.Domain.Repositories
{
    public interface IUserLogRepository
    {
        Task AddAsync(UserLog log);
    }
}
