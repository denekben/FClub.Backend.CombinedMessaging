using Management.Domain.Entities;
using Management.Domain.Repositories;
using Management.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure.Repositories
{
    public class MembershipRepository : IMembershipRepository
    {
        private readonly AppDbContext _context;

        public MembershipRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Membership membership)
        {
            await _context.Memberships.AddAsync(membership);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Memberships.Where(m => m.Id == id).ExecuteDeleteAsync();
        }

        public async Task<Membership?> GetAsync(Guid id, MembershipIncludes includes)
        {
            var query = _context.Memberships.Where(m => m.Id == id);

            if (includes.HasFlag(MembershipIncludes.Client))
                query = query.Include(m => m.Client);
            if (includes.HasFlag(MembershipIncludes.Tariff))
                query = query.Include(m => m.Tariff);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Membership?> GetAsync(Guid id)
        {
            return await _context.Memberships.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
