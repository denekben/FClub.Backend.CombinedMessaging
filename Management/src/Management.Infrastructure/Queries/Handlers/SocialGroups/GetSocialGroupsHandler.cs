using Management.Application.UseCases.SocialGroups.Queries;
using Management.Domain.DTOs;
using Management.Domain.DTOs.Mappers;
using Management.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure.Queries.Handlers.SocialGroups
{
    public sealed class GetSocialGroupsHandler : IRequestHandler<GetSocialGroups, List<SocialGroupDto>?>
    {
        private readonly AppDbContext _context;

        public GetSocialGroupsHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SocialGroupDto>?> Handle(GetSocialGroups query, CancellationToken cancellationToken)
        {
            var (nameSearchPhrase, sortByCreatedDate, pageNumber, pageSize) = query;

            var groups = _context.SocialGroups.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nameSearchPhrase))
                groups = groups.Where(g => EF.Functions.ILike(g.Name, $"%{nameSearchPhrase}%"));

            groups = sortByCreatedDate switch
            {
                true => groups.OrderBy(g => g.CreatedDate),
                false => groups.OrderByDescending(g => g.CreatedDate),
                _ => groups
            };

            int skipSize = (pageNumber - 1) * pageSize;

            groups = groups.Skip(skipSize).Take(pageSize);

            return await groups.Select(g => g.AsDto()).ToListAsync();
        }
    }
}
