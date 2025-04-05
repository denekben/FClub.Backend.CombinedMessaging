using Management.Domain.Entities;

namespace Management.Domain.Repositories
{
    public interface IStatisticRepository
    {
        Task<StatisticNote?> GetLatestNoteAsync();
        Task AddAsync(StatisticNote stat);
        Task UpdateAsync(StatisticNote stat);
    }
}
