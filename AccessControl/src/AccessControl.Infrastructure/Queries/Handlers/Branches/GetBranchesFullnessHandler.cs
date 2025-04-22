using AccessControl.Application.UseCases.Branches.Queries;
using AccessControl.Domain.DTOs;
using AccessControl.Domain.DTOs.Mappers;
using AccessControl.Infrastructure.Data;
using AccessControll.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Infrastructure.Queries.Handlers.Branches
{
    public sealed class GetBranchesFullnessHandler : IRequestHandler<GetBranchesFullness, List<BranchDto>?>
    {
        private readonly AppDbContext _context;

        public GetBranchesFullnessHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<BranchDto>?> Handle(GetBranchesFullness query, CancellationToken cancellationToken)
        {
            var (nameSearchPhrase, sortByCurrentClientQuantity, sortByMaxOccupancy, sortByCreatedDate, pageNumber, pageSize) = query;

            var fullness = _context.Branches.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nameSearchPhrase))
                fullness = fullness.Where(b => EF.Functions.ILike(b.Name ?? string.Empty, $"%{nameSearchPhrase.Trim()}%"));

            IOrderedQueryable<Branch>? orderedfullness = null;
            orderedfullness = sortByCreatedDate switch
            {
                true => fullness.OrderBy(b => b.CurrentClientQuantity),
                false => fullness.OrderByDescending(b => b.CurrentClientQuantity),
                _ => fullness.OrderBy(b => b.Id)
            };

            orderedfullness = sortByMaxOccupancy switch
            {
                true => orderedfullness.ThenBy(b => b.MaxOccupancy),
                false => orderedfullness.OrderByDescending(b => b.MaxOccupancy),
                _ => orderedfullness
            };

            orderedfullness = sortByCreatedDate switch
            {
                true => orderedfullness.OrderBy(b => b.CreatedDate),
                false => orderedfullness.OrderByDescending(b => b.CreatedDate),
                _ => orderedfullness
            };

            int skipNumber = (pageNumber - 1) * pageSize;

            var pagedfullness = orderedfullness.Skip(skipNumber).Take(pageSize);

            return await pagedfullness.Select(f => f.AsDto()).ToListAsync();
        }
    }
}