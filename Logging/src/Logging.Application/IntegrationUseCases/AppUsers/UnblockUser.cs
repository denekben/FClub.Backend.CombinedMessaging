using MediatR;

namespace Logging.Application.IntegrationUseCases.AppUsers
{
    public sealed record UnblockUser(Guid UserId) : IRequest;
}
