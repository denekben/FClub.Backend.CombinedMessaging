using FClub.Backend.Common.HttpMessaging;
using Management.Application.Services;
using Management.Shared.IntegrationUseCases.AccessControl.Branches;
using Management.Shared.IntegrationUseCases.AccessControl.Clients;
using Management.Shared.IntegrationUseCases.AccessControl.Memberships;
using Management.Shared.IntegrationUseCases.AccessControl.Services;
using Management.Shared.IntegrationUseCases.AccessControl.Tariffs;
using Microsoft.Extensions.DependencyInjection;

namespace Management.Infrastructure.Services
{
    public class HttpAccessControlClient : IHttpAccessControlClient
    {
        private readonly IHttpClientService _httpClient;
        private readonly string _basePath;

        public HttpAccessControlClient([FromKeyedServices("AccessControl")] IHttpClientService httpClient)
        {
            _httpClient = httpClient;
            _basePath = "/api";
        }

        public async Task CreateBranch(CreateBranch command)
        {
            await _httpClient.SendResponse($"{_basePath}/branches", command, RequestType.Post);
        }

        public async Task CreateClient(CreateClient command)
        {
            await _httpClient.SendResponse($"{_basePath}/clients", command, RequestType.Post);
        }

        public async Task CreateMembership(CreateMembership command)
        {
            await _httpClient.SendResponse($"{_basePath}/memberships", command, RequestType.Post);
        }

        public async Task CreateService(CreateService command)
        {
            await _httpClient.SendResponse($"{_basePath}/services", command, RequestType.Post);
        }

        public async Task CreateTariff(CreateTariff command)
        {
            await _httpClient.SendResponse($"{_basePath}/tariffs", command, RequestType.Post);
        }

        public async Task DeleteBranch(DeleteBranch command)
        {
            await _httpClient.SendResponse($"{_basePath}/branches", command, RequestType.Delete);
        }

        public async Task DeleteClient(DeleteClient command)
        {
            await _httpClient.SendResponse($"{_basePath}/clients", command, RequestType.Delete);
        }

        public async Task DeleteMembership(DeleteMembership command)
        {
            await _httpClient.SendResponse($"{_basePath}/memberships", command, RequestType.Delete);
        }

        public async Task DeleteService(DeleteService command)
        {
            await _httpClient.SendResponse($"{_basePath}/services", command, RequestType.Delete);
        }

        public async Task DeleteTariff(DeleteTariff command)
        {
            await _httpClient.SendResponse($"{_basePath}/tariffs", command, RequestType.Delete);
        }

        public async Task RegisterNewUser(RegisterNewUser command)
        {
            await _httpClient.SendResponse($"{_basePath}/clients", command, RequestType.Post);
        }

        public async Task UpdateBranch(UpdateBranch command)
        {
            await _httpClient.SendResponse($"{_basePath}/branches", command, RequestType.Put);
        }

        public async Task UpdateClient(UpdateClient command)
        {
            await _httpClient.SendResponse($"{_basePath}/clients", command, RequestType.Put);
        }

        public async Task UpdateMembership(UpdateMembership command)
        {
            await _httpClient.SendResponse($"{_basePath}/memberships", command, RequestType.Put);
        }

        public async Task UpdateService(UpdateService command)
        {
            await _httpClient.SendResponse($"{_basePath}/services", command, RequestType.Put);
        }

        public async Task UpdateTariff(UpdateTariff command)
        {
            await _httpClient.SendResponse($"{_basePath}/tariffs", command, RequestType.Put);
        }
    }
}
