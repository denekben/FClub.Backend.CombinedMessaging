using AccessControl.Domain.Repositories;
using AccessControl.Infrastructure.Data;

namespace AccessControl.Infrastructure.Repositories
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
