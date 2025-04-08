using Management.Shared.IntegrationUseCases.Notifications.Clients;

namespace Management.Application.Services
{
    public interface IHttpNotificationsClient
    {
        Task CreateClient(CreateClient command);
        Task DeleteClient(DeleteClient command);
        Task UpdateClient(UpdateClient command);
    }
}
