using AccessControl.Domain.Entities;

namespace AccessControl.Domain.Repositories
{
    public interface IEntryLogRepository
    {
        Task AddAsync(EntryLog log);
    }
}
