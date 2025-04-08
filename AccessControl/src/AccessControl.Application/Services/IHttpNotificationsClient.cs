using AccessControl.Shared.IntegrationUseCases.Notifications.Clients;

namespace AccessControl.Application.Services
{
    public interface IHttpNotificationsClient
    {
        Task GoThrough(GoThrough command);
    }
}