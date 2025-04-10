using AccessControl.Domain.Repositories;
using AccessControl.Infrastructure.Data;
using AccessControll.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Infrastructure.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly AppDbContext _context;

        public BranchRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Branch branch)
        {
            await _context.Branches.AddAsync(branch);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Branches.Where(b => b.Id == id).ExecuteDeleteAsync();
        }

        public async Task<Branch?> GetAsync(Guid id, BranchIncludes includes)
        {
            var query = _context.Branches.Where(b => b.Id == id);
            if (includes.HasFlag(BranchIncludes.ServicesBranches))
                query = query.Include(b => b.ServiceBranches);

            return await query.FirstOrDefaultAsync();
        }
    }
}