using MediatR;

namespace Management.Application.UseCases.AppUsers.Commands
{
    public sealed record BlockUser(Guid UserId) : IRequest;
}
