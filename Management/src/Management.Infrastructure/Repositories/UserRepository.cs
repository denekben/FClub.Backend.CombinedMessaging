using Management.Domain.Entities;
using Management.Domain.Repositories;
using Management.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure.Repositories
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

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.AppUsers.AnyAsync(u => u.Email == email);
        }

        public async Task<AppUser?> GetAsync(Guid id)
        {
            return await _context.AppUsers.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<AppUser?> GetAsync(Guid id, UserIncludes includes)
        {
            var query = _context.AppUsers.Where(u => u.Id == id);

            if (includes.HasFlag(UserIncludes.Role))
                query = query.Include(u => u.Role);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<AppUser?> GetUserByEmailAsync(string email, UserIncludes includes)
        {
            var query = _context.AppUsers.Where(u => u.Email == email);

            if (includes.HasFlag(UserIncludes.Role))
                query = query.Include(u => u.Role);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool?> IsBlocked(Guid id)
        {
            return (await _context.AppUsers.FirstOrDefaultAsync(u => u.Id == id))?.IsBlocked;
        }
    }
}
