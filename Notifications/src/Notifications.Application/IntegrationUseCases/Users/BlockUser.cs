using MediatR;

namespace Notifications.Application.IntegrationUseCases.Users
{
    public sealed record BlockUser(Guid UserId) : IRequest;

}
