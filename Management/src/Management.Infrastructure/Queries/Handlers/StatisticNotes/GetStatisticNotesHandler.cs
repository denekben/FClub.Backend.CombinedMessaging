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

            var notes = _context.StatisticNotes.Where(n => n.BranchId == branchId).AsQueryable();

            notes = notes.Where(n => n.CreatedDate >= startDate && n.CreatedDate <= endDate);

            notes = notes.OrderBy(n => n.CreatedDate);

            return await notes.Select(n => n.AsDto()).ToListAsync();
        }
    }
}