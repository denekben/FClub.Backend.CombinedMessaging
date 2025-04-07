using Management.Domain.Entities;

namespace Management.Domain.Repositories
{
    public interface IStatisticRepository
    {
        Task AddAsync(StatisticNote stat);
    }
}
