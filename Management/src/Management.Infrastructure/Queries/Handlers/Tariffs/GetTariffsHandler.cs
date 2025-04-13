using Management.Application.UseCases.Tariffs.Queries;
using Management.Domain.DTOs;
using Management.Domain.DTOs.Mappers;
using Management.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure.Queries.Handlers.Tariffs
{
    public sealed class GetTariffsHandler : IRequestHandler<GetTariffs, List<TariffDto>?>
    {
        private readonly AppDbContext _context;

        public GetTariffsHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TariffDto>?> Handle(GetTariffs query, CancellationToken cancellationToken)
        {
            var (nameSearchPhrase, sortByCreatedDate, pageNumber, pageSize) = query;

            var tariffs = _context.Tariffs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nameSearchPhrase))
                tariffs = tariffs.Where(t => EF.Functions.ILike(t.Name, $"%{nameSearchPhrase}%"));

            tariffs = sortByCreatedDate switch
            {
                true => tariffs.OrderBy(t => t.CreatedDate),
                false => tariffs.OrderByDescending(t => t.CreatedDate),
                _ => tariffs
            };

            int skipSize = (pageNumber - 1) * pageSize;

            tariffs = tariffs.Skip(skipSize).Take(pageSize);

            tariffs = tariffs.Include(t => t.ServiceTariffs).ThenInclude(st => st.Service);

            return await tariffs.Select(t => t.AsDto(t.ServiceTariffs.Select(st => st.Service).ToList())).ToListAsync();
        }
    }
}