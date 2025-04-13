using FClub.Backend.Common.HttpMessaging;
using FClub.Backend.Common.Services;
using Management.Application.Services;
using Management.Shared.IntegrationUseCases.AccessControl.Branches;
using Management.Shared.IntegrationUseCases.AccessControl.Clients;
using Management.Shared.IntegrationUseCases.AccessControl.Memberships;
using Management.Shared.IntegrationUseCases.AccessControl.Services;
using Management.Shared.IntegrationUseCases.AccessControl.Tariffs;
using Management.Shared.IntegrationUseCases.AccessControl.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Management.Infrastructure.Services
{
    public class HttpAccessControlClient : IHttpAccessControlClient
    {
        private readonly IHttpClientService _httpClient;
        private readonly string _token;
        private readonly string _basePath;

        public HttpAccessControlClient([FromKeyedServices("AccessControl")] IHttpClientService httpClient, ITokenService tokenService)
        {
            _httpClient = httpClient;
            _basePath = "/api/access-control/internal";
            _token = tokenService.GenerateInternalAccessToken();
        }

        public async Task BlockUser(BlockUser command)
        {
            await _httpClient.SendResponse($"{_basePath}/users/{command.UserId}/block", command, RequestType.Put, _token);
        }

        public async Task CreateBranch(CreateBranch command)
        {
            await _httpClient.SendResponse($"{_basePath}/branches", command, RequestType.Post, _token);
        }

        public async Task CreateClient(CreateClient command)
        {
            await _httpClient.SendResponse($"{_basePath}/clients", command, RequestType.Post, _token);
        }

        public async Task CreateMembership(CreateMembership command)
        {
            await _httpClient.SendResponse($"{_basePath}/memberships", command, RequestType.Post, _token);
        }

        public async Task CreateService(CreateService command)
        {
            await _httpClient.SendResponse($"{_basePath}/services", command, RequestType.Post, _token);
        }

        public async Task CreateTariff(CreateTariff command)
        {
            await _httpClient.SendResponse($"{_basePath}/tariffs", command, RequestType.Post, _token);
        }

        public async Task DeleteBranch(DeleteBranch command)
        {
            await _httpClient.SendResponse($"{_basePath}/branches/{command.Id}", command, RequestType.Delete, _token);
        }

        public async Task DeleteClient(DeleteClient command)
        {
            await _httpClient.SendResponse($"{_basePath}/clients/{command.Id}", command, RequestType.Delete, _token);
        }

        public async Task DeleteMembership(DeleteMembership command)
        {
            await _httpClient.SendResponse($"{_basePath}/memberships/{command.MembershipId}", command, RequestType.Delete, _token);
        }

        public async Task DeleteService(DeleteService command)
        {
            await _httpClient.SendResponse($"{_basePath}/services/{command.Id}", command, RequestType.Delete, _token);
        }

        public async Task DeleteTariff(DeleteTariff command)
        {
            await _httpClient.SendResponse($"{_basePath}/tariffs/{command.Id}", command, RequestType.Delete, _token);
        }

        public async Task RegisterNewUser(RegisterNewUser command)
        {
            await _httpClient.SendResponse($"{_basePath}/clients/register-user", command, RequestType.Post, _token);
        }

        public async Task UnblockUser(UnblockUser command)
        {
            await _httpClient.SendResponse($"{_basePath}/users/{command.UserId}/unblock", command, RequestType.Put, _token);
        }

        public async Task UpdateBranch(UpdateBranch command)
        {
            await _httpClient.SendResponse($"{_basePath}/branches", command, RequestType.Put, _token);
        }

        public async Task UpdateClient(UpdateClient command)
        {
            await _httpClient.SendResponse($"{_basePath}/clients", command, RequestType.Put, _token);
        }

        public async Task UpdateMembership(UpdateMembership command)
        {
            await _httpClient.SendResponse($"{_basePath}/memberships", command, RequestType.Put, _token);
        }

        public async Task UpdateService(UpdateService command)
        {
            await _httpClient.SendResponse($"{_basePath}/services", command, RequestType.Put, _token);
        }

        public async Task UpdateTariff(UpdateTariff command)
        {
            await _httpClient.SendResponse($"{_basePath}/tariffs", command, RequestType.Put, _token);
        }
    }
}
