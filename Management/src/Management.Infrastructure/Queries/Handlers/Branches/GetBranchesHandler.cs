using Management.Application.UseCases.Branches.Queries;
using Management.Domain.DTOs;
using Management.Domain.DTOs.Mappers;
using Management.Domain.Entities;
using Management.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure.Queries.Handlers.Branches
{
    public sealed class GetBranchesHandler : IRequestHandler<GetBranches, List<BranchDto>?>
    {
        private readonly AppDbContext _context;

        public GetBranchesHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<BranchDto>?> Handle(GetBranches query, CancellationToken cancellationToken)
        {
            var (nameSearchPhrase, addressSearchPhrase, sortByMaxOccupancy, sortByCreatedDate, pageNumber, pageSize) = query;

            var branches = _context.Branches.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nameSearchPhrase))
                branches = branches.Where(b => EF.Functions.ILike(b.Name ?? string.Empty, $"%{nameSearchPhrase}%"));

            if (!string.IsNullOrWhiteSpace(addressSearchPhrase))
                branches = branches.Where(b => EF.Functions.ILike(b.Address.ToString(), $"%{addressSearchPhrase}%"));

            IOrderedQueryable<Branch>? orderedBranches = null;
            orderedBranches = sortByMaxOccupancy switch
            {
                true => branches.OrderBy(b => b.MaxOccupancy),
                false => branches.OrderByDescending(b => b.MaxOccupancy),
                _ => branches.OrderBy(b => b.Id)
            };

            orderedBranches = sortByCreatedDate switch
            {
                true => orderedBranches.OrderBy(b => b.CreatedDate),
                false => orderedBranches.OrderByDescending(b => b.CreatedDate),
                _ => orderedBranches
            };

            int skipSize = (pageNumber - 1) * pageSize;

            var pagedBranches = orderedBranches.Skip(skipSize).Take(pageSize);

            pagedBranches = pagedBranches.Include(b => b.ServiceBranches).ThenInclude(sb => sb.Service);

            return (await pagedBranches.ToListAsync())
                .Select(b => b.AsDto(b.ServiceBranches.Select(sb => sb.Service).ToList()))
                .ToList();
        }
    }
}