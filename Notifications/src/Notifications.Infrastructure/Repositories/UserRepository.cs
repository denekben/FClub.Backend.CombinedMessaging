using Microsoft.EntityFrameworkCore;
using Notifications.Domain.Entities;
using Notifications.Domain.Repositories;
using Notifications.Infrastructure.Data;

namespace Notifications.Infrastructure.Repositories
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
            await _context.Users.AddAsync(user);
        }

        public async Task<AppUser?> GetAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool?> IsBlocked(Guid id)
        {
            return (await _context.Users.FirstOrDefaultAsync(u => u.Id == id))?.IsBlocked;
        }
    }
}
