using Management.Application.UseCases.StatisticNotes.Queries;
using Management.Domain.DTOs;
using Management.Domain.DTOs.Mappers;
using Management.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure.Queries.Handlers.StatisticNotes
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
                        TotalCost = g.Sum(n => n.MembershipCost),
                        TotalQuantity = g.Sum(n => n.MembershipQuantity)
                    })
                    .OrderBy(x => x.Date)
                    .ToListAsync(cancellationToken);

                return groupedData.Select(x => new StatisticNoteDto(
                    x.Date,
                    x.TotalCost,
                    x.TotalQuantity
                )).ToList();
            }
        }
    }
}