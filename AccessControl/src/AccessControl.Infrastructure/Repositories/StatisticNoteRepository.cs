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
            var now = DateTime.Now;
            var currentNoteTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0);
            var currentStat = _context.StatisticNotes.FirstOrDefault(
                s => s.CreatedDate == currentNoteTime && s.BranchId == statisticNote.BranchId);
            if (currentStat != null)
            {
                currentStat.EntriesQuantity += statisticNote.EntriesQuantity;
            }
            else
            {
                await _context.StatisticNotes.AddAsync(statisticNote);
            }
        }
    }
}