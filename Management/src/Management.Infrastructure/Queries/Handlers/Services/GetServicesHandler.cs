using Management.Application.UseCases.Services.Queries;
using Management.Domain.DTOs;
using Management.Domain.DTOs.Mappers;
using Management.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure.Queries.Handlers.Services
{
    public sealed class GetServicesHandler : IRequestHandler<GetServices, List<ServiceDto>?>
    {
        private readonly AppDbContext _context;

        public GetServicesHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ServiceDto>?> Handle(GetServices query, CancellationToken cancellationToken)
        {
            var (nameSearchPhrase, sortByCreatedDate, pageNumber, pageSize) = query;

            var services = _context.Services.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nameSearchPhrase))
                services = services.Where(s => EF.Functions.ILike(s.Name, $"%{nameSearchPhrase}%"));

            services = sortByCreatedDate switch
            {
                true => services.OrderBy(s => s.CreatedDate),
                false => services.OrderByDescending(s => s.CreatedDate),
                _ => services
            };

            int skipSize = (pageNumber - 1) * pageSize;

            services = services.Skip(skipSize).Take(pageSize);

            return await services.Select(s => s.AsDto()).ToListAsync();
        }
    }
}