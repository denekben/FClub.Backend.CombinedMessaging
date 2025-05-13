using Logging.Domain.Entities;
using Logging.Domain.Repositories;
using Logging.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Logging.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AppUser user)
        {
            await _context.AppUsers.AddAsync(user);
        }

        public async Task<AppUser?> GetAsync(Guid id)
        {
            return await _context.AppUsers.FirstOrDefaultAsync(au => au.Id == id);
        }

        public async Task<bool?> IsBlockedAsync(Guid id)
        {
            return await _context.AppUsers.AnyAsync(u => u.Id == id && u.IsBlocked);
        }
    }
}
