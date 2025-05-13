using Management.Shared.IntegrationUseCases.Logging.Users;

namespace Management.Application.Services
{
    public interface IHttpLoggingClient
    {
        Task RegisterNewUser(RegisterNewUser command);

        Task BlockUser(BlockUser command);
        Task UnblockUser(UnblockUser command);
    }
}
