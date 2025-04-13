using AccessControl.Application.UseCases.ClientLogs.Queries;
using AccessControl.Domain.DTOs;
using AccessControl.Domain.DTOs.Mappers;
using AccessControl.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Infrastructure.Queries.Handlers.EntryLogs
{
    public sealed class GetEntryLogsHandler : IRequestHandler<GetEntryLogs, List<EntryLogDto>?>
    {
        private readonly AppDbContext _context;

        public GetEntryLogsHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EntryLogDto>?> Handle(GetEntryLogs query, CancellationToken cancellationToken)
        {
            var (clientId, turnstileId, clientNameSearchPhrase, branchNameSearchPhrase, serviceNameSearchPhrase, sortByCreatedDate, pageNumber, pageSize) = query;

            var logs = _context.EntryLogs.AsQueryable();

            if (clientId != null)
                logs = logs.Where(l => l.ClientId == clientId);

            if (turnstileId != null)
                logs = logs.Where(l => l.TurnstileId == turnstileId);

            if (!string.IsNullOrWhiteSpace(clientNameSearchPhrase))
                logs = logs.Where(l => EF.Functions.ILike(l.ClientFullName, $"%{clientNameSearchPhrase}%"));

            if (!string.IsNullOrWhiteSpace(branchNameSearchPhrase))
                logs = logs.Where(l => EF.Functions.ILike(l.BranchName, $"%{branchNameSearchPhrase}%"));

            if (!string.IsNullOrWhiteSpace(serviceNameSearchPhrase))
                logs = logs.Where(l => EF.Functions.ILike((l.ServiceName ?? string.Empty), $"%{serviceNameSearchPhrase}%"));

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