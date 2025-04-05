using MediatR;

namespace Notifications.Application.UseCases.UserLogs.Queries
{
    public sealed record GetUserLogsByUserId : IRequest;
}
