using FClub.Backend.Common.HttpMessaging;
using Management.Application.Services;
using Management.Shared.IntegrationUseCases.Notifications.Clients;
using Microsoft.Extensions.DependencyInjection;

namespace Management.Infrastructure.Services
{
    public class HttpNotificationsClient : IHttpNotificationsClient
    {
        private readonly IHttpClientService _httpClient;
        private readonly string _basePath;

        public HttpNotificationsClient([FromKeyedServices("Notifications")] IHttpClientService httpClient)
        {
            _httpClient = httpClient;
            _basePath = "/api/notifications";
        }

        public async Task CreateClient(CreateClient command)
        {
            await _httpClient.SendResponse($"{_basePath}/clients", command, RequestType.Post);
        }

        public async Task DeleteClient(DeleteClient command)
        {
            await _httpClient.SendResponse($"{_basePath}/clients", command, RequestType.Delete);
        }

        public async Task UpdateClient(UpdateClient command)
        {
            await _httpClient.SendResponse($"{_basePath}/clients", command, RequestType.Put);
        }

        public async Task CreateBranch(Shared.IntegrationUseCases.Notifications.Branches.CreateBranch command)
        {
            await _httpClient.SendResponse($"{_basePath}/branches", command, RequestType.Post);
        }

        public async Task CreateTariff(Shared.IntegrationUseCases.Notifications.Tariffs.CreateTariff command)
        {
            await _httpClient.SendResponse($"{_basePath}/tariffs", command, RequestType.Post);
        }
    }
}
