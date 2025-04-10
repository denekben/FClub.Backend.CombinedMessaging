using Management.Domain.Entities;
using Management.Domain.Repositories;
using Management.Infrastructure.Data;

namespace Management.Infrastructure.Repositories
{
    public class StatisticRepository : IStatisticRepository
    {
        private readonly AppDbContext _context;

        public StatisticRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(StatisticNote stat)
        {
            await _context.StatisticNotes.AddAsync(stat);
        }
    }
}
