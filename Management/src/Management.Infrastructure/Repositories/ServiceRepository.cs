using Management.Domain.Repositories;

namespace Management.Infrastructure.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        public Task AddAsync(Domain.Entities.Service service)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AllExistsAsync(List<Guid> id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AllExistsByNameAsync(List<string> serviceNames)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOneBranchServicesAsync(List<Guid> serviceIds)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOneBranchServicesByNameAsync(List<string> servicesName, Guid branchId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.Service?> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.Service?> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Domain.Entities.Service service)
        {
            throw new NotImplementedException();
        }
    }
}
