using FClub.Backend.Common.Exceptions;
using Management.Application.UseCases.Tariffs.Queries;
using Management.Domain.DTOs;
using Management.Domain.DTOs.Mappers;
using Management.Domain.Entities;
using Management.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure.Queries.Handlers.Tariffs
{
    public sealed class GetTariffsHandler : IRequestHandler<GetTariffs, List<TariffWithGroupsDto>?>
    {
        private readonly AppDbContext _context;

        public GetTariffsHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TariffWithGroupsDto>?> Handle(GetTariffs query, CancellationToken cancellationToken)
        {
            var (nameSearchPhrase, sortByCreatedDate, pageNumber, pageSize) = query;

            var tariffs = _context.Tariffs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nameSearchPhrase))
                tariffs = tariffs.Where(t => EF.Functions.ILike(t.Name, $"%{nameSearchPhrase.Trim()}%"));

            tariffs = sortByCreatedDate switch
            {
                true => tariffs.OrderBy(t => t.CreatedDate),
                false => tariffs.OrderByDescending(t => t.CreatedDate),
                _ => tariffs
            };

            int skipSize = (pageNumber - 1) * pageSize;

            tariffs = tariffs.Skip(skipSize).Take(pageSize);

            tariffs = tariffs.Include(t => t.ServiceTariffs).ThenInclude(st => st.Service);

            var resultTariffs = await tariffs.ToListAsync();

            List<TariffWithGroupsDto> returnTariffs = [];
            foreach (var resultTariff in resultTariffs)
            {
                Dictionary<SocialGroup, int> discounts = [];
                var discs = resultTariff.DiscountForSocialGroup ?? [];
                foreach (var disc in discs)
                {
                    var group = await _context.SocialGroups.FirstOrDefaultAsync(g => g.Id == disc.Key) ?? throw new NotFoundException($"Cannot find group {disc.Key}");
                    discounts.Add(group, disc.Value);
                }
                returnTariffs.Add(resultTariff.AsDto(resultTariff.ServiceTariffs.Select(st => st.Service).ToList(), discounts));
            }

            return returnTariffs;
        }
    }
}