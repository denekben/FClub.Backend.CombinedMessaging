using AccessControl.Application.UseCases.UserLogs.Queries;
using AccessControl.Domain.DTOs;
using AccessControl.Domain.DTOs.Mappers;
using AccessControl.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Infrastructure.Queries.Handlers.UserLogs
{
    public sealed class GetUserLogsHandler : IRequestHandler<GetUserLogs, List<UserLogDto>?>
    {
        private readonly AppDbContext _context;

        public GetUserLogsHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserLogDto>?> Handle(GetUserLogs query, CancellationToken cancellationToken)
        {
            var (userId, textSearchPhrase, sortByCreatedDate, pageNumber, pageSize) = query;

            var logs = _context.UserLogs.Where(l => l.AppUserId == userId).AsQueryable();

            if (!string.IsNullOrWhiteSpace(textSearchPhrase))
                logs = logs.Where(l => EF.Functions.ILike(l.Text, $"%{textSearchPhrase}%"));

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