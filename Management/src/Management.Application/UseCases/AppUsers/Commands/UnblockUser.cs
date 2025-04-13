using MediatR;

namespace Management.Application.UseCases.AppUsers.Commands
{
    public sealed record UnblockUser(Guid UserId) : IRequest;
}
