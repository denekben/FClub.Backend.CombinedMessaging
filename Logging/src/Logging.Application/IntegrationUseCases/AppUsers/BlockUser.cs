using MediatR;

namespace Logging.Application.IntegrationUseCases.AppUsers
{
    public sealed record BlockUser(Guid UserId) : IRequest;
}
