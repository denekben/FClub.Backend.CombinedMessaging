using AccessControl.Application.Services;
using AccessControl.Shared.IntegrationUseCases.Notifications.Clients;
using FClub.Backend.Common.HttpMessaging;
using Microsoft.Extensions.DependencyInjection;

namespace AccessControl.Infrastructure.Services
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

        public async Task GoThrough(GoThrough command)
        {
            await _httpClient.SendResponse($"{_basePath}/go-through", command, RequestType.Put);
        }
    }
}
