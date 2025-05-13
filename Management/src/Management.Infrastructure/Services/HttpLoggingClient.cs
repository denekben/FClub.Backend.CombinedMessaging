using FClub.Backend.Common.HttpMessaging;
using FClub.Backend.Common.Services;
using Management.Application.Services;
using Management.Shared.IntegrationUseCases.Logging.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Management.Infrastructure.Services
{
    public class HttpLoggingClient : IHttpLoggingClient
    {
        private readonly IHttpClientService _httpClient;
        private readonly string _basePath;
        private readonly string _token;

        public HttpLoggingClient([FromKeyedServices("Logging")] IHttpClientService httpClient, ITokenService tokenService)
        {
            _httpClient = httpClient;
            _basePath = "/api/logging/internal";
            _token = tokenService.GenerateInternalAccessToken();
        }

        public async Task BlockUser(BlockUser command)
        {
            await _httpClient.SendResponse($"{_basePath}/users/{command.UserId}/block", command, RequestType.Put, _token);
        }

        public async Task RegisterNewUser(RegisterNewUser command)
        {
            await _httpClient.SendResponse($"{_basePath}/users/register-user", command, RequestType.Post, _token);
        }

        public async Task UnblockUser(UnblockUser command)
        {
            await _httpClient.SendResponse($"{_basePath}/users/{command.UserId}/unblock", command, RequestType.Put, _token);
        }
    }
}
