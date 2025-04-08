using AccessControl.Domain.Entities;
using AccessControl.Domain.Repositories;

namespace AccessControl.Infrastructure.Repositories
{
    public class TurnstileRepository : ITurnstileRepository
    {
        public Task AddAsync(Turnstile turnstile)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Turnstile?> GetAsync(Guid id, TurnistileIncludes includes)
        {
            throw new NotImplementedException();
        }

        public Task<Turnstile?> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Turnstile turnstile)
        {
            throw new NotImplementedException();
        }
    }
}
