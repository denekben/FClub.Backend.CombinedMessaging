using AccessControl.Domain.Entities;

namespace AccessControl.Domain.Repositories
{
    public interface ITurnstileRepository
    {
        Task<Turnstile?> GetAsync(Guid id, TurnistileIncludes includes);
        Task AddAsync(Turnstile turnstile);
        Task UpdateAsync(Turnstile turnstile);
        Task DeleteAsync(Guid id);
    }

    [Flags]
    public enum TurnistileIncludes
    {
        Branches = 1,
        Services = 2
    }
}
