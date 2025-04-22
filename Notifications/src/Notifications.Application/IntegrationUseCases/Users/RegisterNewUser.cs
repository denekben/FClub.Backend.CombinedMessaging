using MediatR;

namespace Notifications.Application.IntegrationUseCases.Users
{
    public sealed record RegisterNewUser(Guid UserId) : IRequest;
}
