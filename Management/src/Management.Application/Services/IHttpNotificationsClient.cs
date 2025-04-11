using Management.Shared.IntegrationUseCases.Notifications.Branches;
using Management.Shared.IntegrationUseCases.Notifications.Clients;
using Management.Shared.IntegrationUseCases.Notifications.Tariffs;

namespace Management.Application.Services
{
    public interface IHttpNotificationsClient
    {
        Task CreateClient(CreateClient command);
        Task DeleteClient(DeleteClient command);
        Task UpdateClient(UpdateClient command);

        Task CreateBranch(CreateBranch command);

        Task CreateTariff(CreateTariff command);
    }
}
