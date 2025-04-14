using AccessControl.Application.UseCases.Branches.Queries;
using AccessControl.Domain.DTOs;
using AccessControl.Domain.DTOs.Mappers;
using AccessControl.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Infrastructure.Queries.Handlers.Branches
{
    public sealed class GetBranchFullnessHandler : IRequestHandler<GetBranchFullness, BranchDto?>
    {
        private readonly AppDbContext _context;

        public GetBranchFullnessHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BranchDto?> Handle(GetBranchFullness query, CancellationToken cancellationToken)
        {
            return (await _context.Branches.FirstOrDefaultAsync(b => b.Id == query.BranchId))?.AsDto();
        }
    }
}
