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
            var now = DateTime.Now;
            var currentNoteTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0);
            var currentStat = _context.StatisticNotes.FirstOrDefault(
                s => s.CreatedDate == currentNoteTime && s.BranchId == stat.BranchId);
            if (currentStat != null)
            {
                currentStat.MembershipCost += stat.MembershipCost;
                currentStat.MembershipQuantity += stat.MembershipQuantity;
            }
            else
            {
                await _context.StatisticNotes.AddAsync(stat);
            }
        }
    }
}
