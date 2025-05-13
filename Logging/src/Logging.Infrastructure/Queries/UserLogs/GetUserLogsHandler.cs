using FClub.Backend.Common.Logging;
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
    public sealed class GetUserLogsHandler : IRequestHandler<GetUserLogs, List<UserLogDto>?>
    {
        private const double _cacheExpirationMinutes = 5;
        private readonly IDistributedCache _cache;
        private readonly AppDbContext _context;

        public GetUserLogsHandler(AppDbContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<List<UserLogDto>?> Handle(GetUserLogs query, CancellationToken cancellationToken)
        {
            var (userId, textSearchPhrase, sortByCreatedDate, pageNumber, pageSize) = query;

            var cacheKey = $"logs:{userId}:{textSearchPhrase}:{sortByCreatedDate}:{pageNumber}:{pageSize}";

            var cachedData = await _cache.GetStringAsync(cacheKey, cancellationToken);
            if (cachedData != null)
            {
                return JsonSerializer.Deserialize<List<UserLogDto>>(cachedData);
            }

            var logs = _context.UserLogs.AsQueryable();

            logs = logs.Where(l => l.AppUserId == userId);

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
                JsonSerializer.Serialize(logsResult),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_cacheExpirationMinutes)
                },
                cancellationToken);

            return logsResult;
        }
    }
}