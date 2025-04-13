using MediatR;

namespace AccessControl.Application.IntegrationUseCases.Users
{
    public sealed record UnblockUser(Guid UserId) : IRequest;
}
