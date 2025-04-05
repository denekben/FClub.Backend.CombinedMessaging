using AccessControl.Application.UseCases.ClientLogs.Queries;
using MediatR;

namespace AccessControl.Infrastructure.Queries.Handlers.EntryLogs
{
    public sealed class GetEntryLogsByClientIdHandler : IRequestHandler<GetEntryLogsByClientId,>
    {
    }
}
