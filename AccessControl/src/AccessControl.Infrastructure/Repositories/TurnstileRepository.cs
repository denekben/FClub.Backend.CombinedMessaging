using AccessControl.Domain.Entities;
using AccessControl.Domain.Repositories;
using AccessControl.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Infrastructure.Repositories
{
    public class TurnstileRepository : ITurnstileRepository
    {
        private readonly AppDbContext _context;

        public TurnstileRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Turnstile turnstile)
        {
            await _context.Turnstiles.AddAsync(turnstile);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Turnstiles.Where(t => t.Id == id).ExecuteDeleteAsync();
        }

        public async Task<Turnstile?> GetAsync(Guid id, TurnistileIncludes includes)
        {
            var query = _context.Turnstiles.Where(t => t.Id == id);

            if (includes.HasFlag(TurnistileIncludes.Services))
            {
                query = query
                    .Include(t => t.Branch)
                    .ThenInclude(b => b.ServiceBranches)
                    .ThenInclude(sb => sb.Service);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Turnstile?> GetAsync(Guid id)
        {
            return await _context.Turnstiles.FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
