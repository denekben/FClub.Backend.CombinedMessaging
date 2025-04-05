using Management.Domain.Entities;
using Management.Domain.Repositories;

namespace Management.Infrastructure.Repositories
{
    public class StatisticRepository : IStatisticRepository
    {
        public Task AddAsync(StatisticNote stat)
        {
            throw new NotImplementedException();
        }

        public Task<StatisticNote?> GetLatestNoteAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(StatisticNote stat)
        {
            throw new NotImplementedException();
        }
    }
}
