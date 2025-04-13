using MediatR;

namespace AccessControl.Application.IntegrationUseCases.Users
{
    public sealed record BlockUser(Guid UserId) : IRequest;
}
