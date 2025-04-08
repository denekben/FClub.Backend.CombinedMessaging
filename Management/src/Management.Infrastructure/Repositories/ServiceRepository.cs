using Management.Domain.Repositories;

namespace Management.Infrastructure.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        public Task AddAsync(Domain.Entities.Service service)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOneBranchAndZeroTariffsServicesAsync(List<Guid> serviceIds)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOneBranchAndZeroTariffsServicesByNameAsync(List<string> servicesName, Guid branchId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOneTariffAndZeroBranchesServicesAsync(List<Guid> serviceIds)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOneTariffAndZeroBranchesServicesByNameAsync(List<string> servicesName, Guid tariffId)
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
