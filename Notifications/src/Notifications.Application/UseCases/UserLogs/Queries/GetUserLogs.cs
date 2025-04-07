using MediatR;
using Notifications.Domain.DTOs;

namespace Notifications.Application.UseCases.UserLogs.Queries
{
    public sealed record GetUserLogs : IRequest<List<UserLogDto>?>;
}
