using Management.Application.UseCases.UserLogs.Queries;
using Management.Domain.DTOs;
using Management.Domain.DTOs.Mappers;
using Management.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure.Queries.Handlers.UserLogs
{
    public sealed class GetLogsHandler : IRequestHandler<GetLogs, List<UserLogDto>?>
    {
        private readonly AppLogDbContext _context;

        public GetLogsHandler(AppLogDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserLogDto>?> Handle(GetLogs query, CancellationToken cancellationToken)
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