using AccessControll.Domain.Entities;

namespace AccessControl.Domain.Repositories
{
    public interface IServiceRepository
    {
        Task UpdateAsync(Service service);
        Task<Service?> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task AddAsync(Service service);
        Task<List<Service>?> GetByBranchId(Guid id);
        Task<List<Service>?> GetByTariffId(Guid id);
        Task DeleteOneBranchAndZeroTariffsServicesAsync(List<Guid> serviceIds);
        Task DeleteOneTariffAndZeroBranchesServicesAsync(List<Guid> serviceIds);
        Task DeleteOneBranchAndZeroTariffsServicesByNameAsync(List<string> servicesName, Guid branchId);
        Task DeleteOneTariffAndZeroBranchesServicesByNameAsync(List<string> servicesName, Guid tariffId);
    }
}
