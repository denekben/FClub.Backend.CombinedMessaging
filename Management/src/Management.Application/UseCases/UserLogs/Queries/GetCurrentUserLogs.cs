using Management.Domain.DTOs;
using MediatR;

namespace Management.Application.UseCases.UserLogs.Queries
{
    public sealed record GetCurrentUserLogs : IRequest<List<UserLogDto>?>;
}
