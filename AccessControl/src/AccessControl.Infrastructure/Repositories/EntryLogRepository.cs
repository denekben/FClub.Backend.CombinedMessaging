using AccessControl.Domain.Entities;
using AccessControl.Domain.Repositories;
using AccessControl.Infrastructure.Data;

namespace AccessControl.Infrastructure.Repositories
{
    public class EntryLogRepository : IEntryLogRepository
    {
        private readonly AppDbContext _context;

        public EntryLogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(EntryLog log)
        {
            await _context.EntryLogs.AddAsync(log);
        }
    }
}
