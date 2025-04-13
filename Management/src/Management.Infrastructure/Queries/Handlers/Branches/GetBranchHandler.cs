using Management.Application.UseCases.Branches.Queries;
using Management.Domain.DTOs;
using Management.Domain.DTOs.Mappers;
using Management.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure.Queries.Handlers.Branches
{
    public sealed class GetBranchHandler : IRequestHandler<GetBranch, BranchDto?>
    {
        private readonly AppDbContext _context;

        public GetBranchHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BranchDto?> Handle(GetBranch query, CancellationToken cancellationToken)
        {
            var branch = await _context.Branches.Where(b => b.Id == query.BranchId).Include(b => b.ServiceBranches).ThenInclude(sb => sb.Service).FirstOrDefaultAsync();
            var services = branch?.ServiceBranches.Select(sb => sb.Service).ToList();
            return branch?.AsDto(services);
        }
    }
}
