using AccessControl.Domain.Entities;
using AccessControl.Domain.Repositories;
using AccessControl.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Infrastructure.Repositories
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

        public async Task<Membership?> GetAsync(Guid id)
        {
            return await _context.Memberships.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
