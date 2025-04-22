using MediatR;
using Microsoft.EntityFrameworkCore;
using Notifications.Application.UseCases.UserLogs.Queries;
using Notifications.Domain.DTOs;
using Notifications.Domain.DTOs.Mappers;
using Notifications.Infrastructure.Data;

namespace Notifications.Infrastructure.Queries.Handlers.UserLog
{
    public sealed class GetUserLogsHandler : IRequestHandler<GetUserLogs, List<UserLogDto>?>
    {
        private readonly AppLogDbContext _context;

        public GetUserLogsHandler(AppLogDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserLogDto>?> Handle(GetUserLogs query, CancellationToken cancellationToken)
        {
            var (userId, textSearchPhrase, sortByCreatedDate, pageNumber, pageSize) = query;

            var logs = _context.UserLogs.AsQueryable();

            if (userId != null)
            {
                logs = logs.Where(l => l.AppUserId == userId);
            }

            if (!string.IsNullOrWhiteSpace(textSearchPhrase))
                logs = logs.Where(l => EF.Functions.ILike(l.Text, $"%{textSearchPhrase.Trim()}%"));

            logs = sortByCreatedDate switch
            {
                true => logs.OrderBy(l => l.CreatedDate),
                false => logs.OrderByDescending(l => l.CreatedDate),
                _ => logs
            };

            int skipNumber = (pageNumber - 1) * pageSize;

            logs = logs.Skip(skipNumber).Take(pageSize);

            return await logs.Select(l => l.AsDto()).ToListAsync();
        }
    }
}