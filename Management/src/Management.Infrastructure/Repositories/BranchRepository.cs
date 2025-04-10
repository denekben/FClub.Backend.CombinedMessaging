using Management.Domain.Entities;
using Management.Domain.Repositories;
using Management.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure.Repositories
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

        public async Task<Branch?> GetAsync(Guid id)
        {
            return await _context.Branches.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Branch?> GetAsync(Guid id, BranchIncludes includes)
        {
            var query = _context.Branches.Where(b => b.Id == id);

            if (includes.HasFlag(BranchIncludes.ServiceBranches))
                query = query.Include(b => b.ServiceBranches);
            if (includes.HasFlag(BranchIncludes.Services))
                query = query
                    .Include(b => b.ServiceBranches)
                    .ThenInclude(sb => sb.Service);

            return await query.FirstOrDefaultAsync();
        }
    }
}
