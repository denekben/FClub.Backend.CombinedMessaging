using AccessControl.Domain.Entities;
using AccessControl.Domain.Repositories;

namespace AccessControl.Infrastructure.Repositories
{
    public class EntryLogRepository : IEntryLogRepository
    {
        public Task AddAsync(EntryLog log)
        {
            throw new NotImplementedException();
        }
    }
}
