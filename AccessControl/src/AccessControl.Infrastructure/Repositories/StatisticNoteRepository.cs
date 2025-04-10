using AccessControl.Domain.Entities;
using AccessControl.Domain.Repositories;
using AccessControl.Infrastructure.Data;

namespace AccessControl.Infrastructure.Repositories
{
    public class StatisticNoteRepository : IStatisticNoteRepository
    {
        private readonly AppDbContext _context;

        public StatisticNoteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(StatisticNote statisticNote)
        {
            await _context.AddAsync(statisticNote);
        }
    }
}
