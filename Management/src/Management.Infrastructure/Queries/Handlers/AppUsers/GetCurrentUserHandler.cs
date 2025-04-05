using Management.Application.UseCases.AppUsers.Queries;
using MediatR;

namespace Management.Infrastructure.Queries.Handlers.AppUsers
{
    public sealed class GetCurrentUserHandler : IRequestHandler<GetCurrentUser,>
    {
    }
}
