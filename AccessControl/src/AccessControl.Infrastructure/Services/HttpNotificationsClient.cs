using AccessControl.Application.Services;
using AccessControl.Shared.IntegrationUseCases.Notifications.Clients;
using FClub.Backend.Common.HttpMessaging;
using FClub.Backend.Common.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AccessControl.Infrastructure.Services
{
    public class HttpNotificationsClient : IHttpNotificationsClient
    {
        private readonly IHttpClientService _httpClient;
        private readonly string _basePath;
        private readonly string _token;

        public HttpNotificationsClient([FromKeyedServices("Notifications")] IHttpClientService httpClient, ITokenService tokenService)
        {
            _httpClient = httpClient;
            _basePath = "/api/notifications/internal";
            _token = tokenService.GenerateInternalAccessToken();
        }

        public async Task GoThrough(GoThrough command)
        {
            await _httpClient.SendResponse($"{_basePath}/go-through", command, RequestType.Put, _token);
        }
    }
}
