using MediatR;

namespace Notifications.Application.IntegrationUseCases.AppUsers
{
    public sealed record RegisterNewUser(Guid UserId) : IRequest;
}
