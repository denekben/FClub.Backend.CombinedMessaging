using MediatR;

namespace Notifications.Application.IntegrationUseCases.Users
{
    public sealed record UnblockUser(Guid UserId) : IRequest;
}
