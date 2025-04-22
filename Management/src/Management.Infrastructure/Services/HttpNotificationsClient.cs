using FClub.Backend.Common.HttpMessaging;
using FClub.Backend.Common.Services;
using Management.Application.Services;
using Management.Shared.IntegrationUseCases.Notifications.Clients;
using Management.Shared.IntegrationUseCases.Notifications.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Management.Infrastructure.Services
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

        public async Task CreateClient(CreateClient command)
        {
            await _httpClient.SendResponse($"{_basePath}/clients", command, RequestType.Post, _token);
        }

        public async Task DeleteClient(DeleteClient command)
        {
            await _httpClient.SendResponse($"{_basePath}/clients/{command.clientId}", command, RequestType.Delete, _token);
        }

        public async Task UpdateClient(UpdateClient command)
        {
            await _httpClient.SendResponse($"{_basePath}/clients", command, RequestType.Put, _token);
        }

        public async Task CreateBranch(Shared.IntegrationUseCases.Notifications.Branches.CreateBranch command)
        {
            await _httpClient.SendResponse($"{_basePath}/branches", command, RequestType.Post, _token);
        }

        public async Task CreateTariff(Shared.IntegrationUseCases.Notifications.Tariffs.CreateTariff command)
        {
            await _httpClient.SendResponse($"{_basePath}/tariffs", command, RequestType.Post, _token);
        }

        public async Task BlockUser(BlockUser command)
        {
            await _httpClient.SendResponse($"{_basePath}/users/{command.UserId}/block", command, RequestType.Put, _token);
        }

        public async Task UnblockUser(UnblockUser command)
        {
            await _httpClient.SendResponse($"{_basePath}/users/{command.UserId}/unblock", command, RequestType.Put, _token);
        }

        public async Task RegisterNewUser(RegisterNewUser command)
        {
            await _httpClient.SendResponse($"{_basePath}/users/register-user", command, RequestType.Post, _token);
        }
    }
}
