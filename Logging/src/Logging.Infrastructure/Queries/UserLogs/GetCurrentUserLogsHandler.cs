using FClub.Backend.Common.Exceptions;
using FClub.Backend.Common.Logging;
using FClub.Backend.Common.Services;
using Logging.Application.UseCases.UserLogs;
using Logging.Domain.DTOs;
using Logging.Domain.DTOs.Mappers;
using Logging.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Logging.Infrastructure.Queries.UserLogs
{
    [SkipLogging]
    public sealed class GetCurrentUserLogsHandler : IRequestHandler<GetCurrentUserLogs, List<UserLogDto>?>
    {
        private const double _cacheExpirationMinutes = 5;
        private readonly IDistributedCache _cache;
        private readonly AppDbContext _context;
        private readonly IHttpContextService _contextService;

        public GetCurrentUserLogsHandler(
            AppDbContext context, IHttpContextService contextService,
            IDistributedCache cache)
        {
            _context = context;
            _contextService = contextService;
            _cache = cache;
        }

        public async Task<List<UserLogDto>?> Handle(GetCurrentUserLogs query, CancellationToken cancellationToken)
        {
            var (textSearchPhrase, sortByCreatedDate, pageNumber, pageSize) = query;

            var userId = _contextService.GetCurrentUserId()
                ?? throw new BadRequestException("Invalid authorization header");

            var cacheKey = $"current_logs:{userId}:{textSearchPhrase}:{sortByCreatedDate}:{pageNumber}:{pageSize}";

            var cachedData = await _cache.GetStringAsync(cacheKey, cancellationToken);
            if (cachedData != null)
            {
                return JsonSerializer.Deserialize<List<UserLogDto>>(cachedData);
            }

            var logs = _context.UserLogs.Where(l => l.AppUserId == userId).AsQueryable();

            if (!string.IsNullOrWhiteSpace(textSearchPhrase))
                logs = logs.Where(l => EF.Functions.ILike(l.Text, $"%{textSearchPhrase.Trim()}%"));

            logs = sortByCreatedDate switch
            {
                true => logs.OrderBy(l => l.CreatedDate),
                false => logs.OrderByDescending(l => l.CreatedDate),
                _ => logs
            };

            int skipNumber = (pageNumber - 1) * pageSize;

            logs = logs.Skip(skipNumber).Take(pageSize);

            var logsResult = await logs.Select(l => l.AsDto()).ToListAsync();

            await _cache.SetStringAsync(
                cacheKey,
                JsonSerializer.Serialize(logs),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_cacheExpirationMinutes)
                },
                cancellationToken);

            return logsResult;
        }
    }
}