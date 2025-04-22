using AccessControl.Domain.Entities;

namespace AccessControl.Domain.Repositories
{
    public interface ITurnstileRepository
    {
        Task<Turnstile?> GetAsync(Guid id, TurnistileIncludes includes);
        Task<Turnstile?> GetAsync(Guid id);
        Task AddAsync(Turnstile turnstile);
        Task DeleteAsync(Guid id);
    }

    [Flags]
    public enum TurnistileIncludes
    {
        Services = 1
    }
}
