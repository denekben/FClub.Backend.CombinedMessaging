using Management.Domain.Entities.Pivots;
using Management.Domain.Repositories;
using Management.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure.Repositories
{
    public class ServiceBranchRepository : IServiceBranchRepository
    {
        private readonly AppDbContext _context;

        public ServiceBranchRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ServiceBranch sb)
        {
            await _context.ServiceBranches.AddAsync(sb);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.ServiceBranches.Where(sb => sb.Id == id).ExecuteDeleteAsync();
        }

        public async Task DeleteByBranchId(Guid id)
        {
            await _context.ServiceBranches.Where(sb => sb.BranchId == id).ExecuteDeleteAsync();
        }
    }
}
