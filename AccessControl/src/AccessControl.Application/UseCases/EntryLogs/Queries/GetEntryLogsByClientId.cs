using MediatR;

namespace AccessControl.Application.UseCases.ClientLogs.Queries
{
    public sealed record GetEntryLogsByClientId : IRequest;
}
