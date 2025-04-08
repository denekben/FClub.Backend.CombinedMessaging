using Management.Domain.Entities;

namespace Management.Domain.Repositories
{
    public interface IServiceRepository
    {
        Task<Service?> GetAsync(Guid id);
        Task<Service?> GetByNameAsync(string name);
        Task DeleteOneBranchAndZeroTariffsServicesAsync(List<Guid> serviceIds);
        Task DeleteOneTariffAndZeroBranchesServicesAsync(List<Guid> serviceIds);
        Task DeleteOneBranchAndZeroTariffsServicesByNameAsync(List<string> servicesName, Guid branchId);
        Task DeleteOneTariffAndZeroBranchesServicesByNameAsync(List<string> servicesName, Guid tariffId);

        Task DeleteAsync(Guid id);
        Task AddAsync(Service service);
        Task UpdateAsync(Service service);
    }
}
