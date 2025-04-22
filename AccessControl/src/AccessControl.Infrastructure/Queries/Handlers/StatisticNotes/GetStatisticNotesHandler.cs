using AccessControl.Application.UseCases.StatisticNotes.Queries;
using AccessControl.Domain.DTOs;
using AccessControl.Domain.DTOs.Mappers;
using AccessControl.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Infrastructure.Queries.Handlers.StatisticNotes
{
    public sealed class GetStatisticNotesHandler : IRequestHandler<GetStatisticNotes, List<StatisticNoteDto>?>
    {
        private readonly AppDbContext _context;

        public GetStatisticNotesHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<StatisticNoteDto>?> Handle(GetStatisticNotes query, CancellationToken cancellationToken)
        {
            var (branchId, startDate, endDate) = query;

            var notes = _context.StatisticNotes.Where(n => n.CreatedDate >= startDate && n.CreatedDate <= endDate);

            if (branchId != null)
            {
                var result = await notes.Where(n => n.BranchId == branchId)
                    .OrderBy(n => n.CreatedDate)
                    .ToListAsync();
                return result.Select(n => n.AsDto()).ToList();
            }
            else
            {
                var groupedData = await notes
                    .GroupBy(n => n.CreatedDate)
                    .Select(g => new
                    {
                        Date = g.Key,
                        EntriesQuantity = g.Sum(n => n.EntriesQuantity)
                    })
                    .OrderBy(x => x.Date)
                    .ToListAsync(cancellationToken);

                return groupedData.Select(x => new StatisticNoteDto(
                    x.Date,
                    x.EntriesQuantity
                )).ToList();
            }
        }
    }
}