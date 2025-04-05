using Management.Application.UseCases.UserLogs.Queries;
using MediatR;

namespace Management.Infrastructure.Queries.Handlers.UserLogs
{
    public sealed class GetLogsHandler : IRequestHandler<GetLogs,>
    {
    }
}
