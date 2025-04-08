using AccessControl.Domain.Repositories;
using AccessControll.Domain.Entities;

namespace AccessControl.Infrastructure.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        public Task AddAsync(Service service)
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

        public Task<Service?> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Service>?> GetByBranchId(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Service>?> GetByTariffId(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Service service)
        {
            throw new NotImplementedException();
        }
    }
}
