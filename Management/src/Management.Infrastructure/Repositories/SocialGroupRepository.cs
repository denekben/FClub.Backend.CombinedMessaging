using Management.Domain.Entities;
using Management.Domain.Repositories;
using Management.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure.Repositories
{
    public class SocialGroupRepository : ISocialGroupRepository
    {
        private readonly AppDbContext _context;

        public SocialGroupRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(SocialGroup socialGroup)
        {
            await _context.SocialGroups.AddAsync(socialGroup);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.SocialGroups.Where(sg => sg.Id == id).ExecuteDeleteAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.SocialGroups.AnyAsync(sg => sg.Id == id);
        }

        public async Task<SocialGroup?> GetAsync(Guid id)
        {
            return await _context.SocialGroups.FirstOrDefaultAsync(sg => sg.Id == id);
        }
    }
}
