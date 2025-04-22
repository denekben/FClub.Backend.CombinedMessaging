using Management.Domain.Repositories;
using Management.Infrastructure.Data;

namespace Management.Infrastructure.Repositories
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;
        private readonly AppLogDbContext _logContext;

        public Repository(AppDbContext context, AppLogDbContext logContext)
        {
            _context = context;
            _logContext = logContext;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task SaveLogsAsync()
        {
            await _logContext.SaveChangesAsync();
        }
    }
}
