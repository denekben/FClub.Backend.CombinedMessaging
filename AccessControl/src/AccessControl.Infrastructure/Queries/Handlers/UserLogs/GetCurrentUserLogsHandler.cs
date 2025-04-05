using AccessControl.Application.UseCases.UserLogs.Queries;
using MediatR;

namespace AccessControl.Infrastructure.Queries.Handlers.UserLogs
{
    public sealed class GetCurrentUserLogsHandler : IRequestHandler<GetCurrentUserLogs,>
    {
    }
}
