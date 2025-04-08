using Management.Shared.IntegrationUseCases.AccessControl.Branches;
using Management.Shared.IntegrationUseCases.AccessControl.Clients;
using Management.Shared.IntegrationUseCases.AccessControl.Memberships;
using Management.Shared.IntegrationUseCases.AccessControl.Services;
using Management.Shared.IntegrationUseCases.AccessControl.Tariffs;

namespace Management.Application.Services
{
    public interface IHttpAccessControlClient
    {
        Task CreateBranch(CreateBranch command);
        Task DeleteBranch(DeleteBranch command);
        Task UpdateBranch(UpdateBranch command);

        Task CreateClient(CreateClient command);
        Task DeleteClient(DeleteClient command);
        Task RegisterNewUser(RegisterNewUser command);
        Task UpdateClient(UpdateClient command);

        Task CreateMembership(CreateMembership command);
        Task DeleteMembership(DeleteMembership command);
        Task UpdateMembership(UpdateMembership command);

        Task CreateService(CreateService command);
        Task DeleteService(DeleteService command);
        Task UpdateService(UpdateService command);

        Task CreateTariff(CreateTariff command);
        Task DeleteTariff(DeleteTariff command);
        Task UpdateTariff(UpdateTariff command);
    }
}
