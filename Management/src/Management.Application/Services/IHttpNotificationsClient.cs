using Management.Shared.IntegrationUseCases.Notifications.Branches;
using Management.Shared.IntegrationUseCases.Notifications.Clients;
using Management.Shared.IntegrationUseCases.Notifications.Tariffs;
using Management.Shared.IntegrationUseCases.Notifications.Users;

namespace Management.Application.Services
{
    public interface IHttpNotificationsClient
    {
        Task CreateClient(CreateClient command);
        Task DeleteClient(DeleteClient command);
        Task UpdateClient(UpdateClient command);

        Task RegisterNewUser(RegisterNewUser command);

        Task BlockUser(BlockUser command);
        Task UnblockUser(UnblockUser command);

        Task CreateBranch(CreateBranch command);

        Task CreateTariff(CreateTariff command);
    }
}
