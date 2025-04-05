using AccessControl.Domain.Entities;

namespace AccessControl.Domain.Repositories
{
    public interface IStatisticNoteRepository
    {
        Task AddAsync(StatisticNote statisticNote);
    }
}
