using AccessControl.Domain.Entities;
using AccessControl.Domain.Repositories;
using AccessControl.Shared.Logging;
using FClub.Backend.Common.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;

namespace Management.Application.UseCases.UserLogs
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private static readonly ConcurrentDictionary<Type, bool> _skipLoggingCache = new();
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        private readonly IHttpContextService _contextService;
        private readonly IUserLogRepository _userLogRepository;

        public LoggingBehavior(
            ILogger<LoggingBehavior<TRequest, TResponse>> logger,
            IHttpContextService currentUserService,
            IUserLogRepository userLogRepository)
        {
            _logger = logger;
            _contextService = currentUserService;
            _userLogRepository = userLogRepository;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {

            var shouldSkip = _skipLoggingCache.GetOrAdd(typeof(TRequest), type =>
            {
                var handlerType = typeof(IRequestHandler<TRequest, TResponse>);
                var handlerAssembly = handlerType.Assembly;
                var handlerTypes = handlerAssembly.GetTypes()
                    .Where(t => t.GetInterfaces().Any(i =>
                        i.IsGenericType &&
                        i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) &&
                        i.GetGenericArguments()[0] == type));

                return handlerTypes.Any(t => t.GetCustomAttribute<SkipLoggingAttribute>() != null);
            });

            if (shouldSkip)
            {
                return await next();
            }


            var requestName = typeof(TRequest).Name;
            var userId = _contextService.GetCurrentUserId() ?? Guid.Empty;

            var logText = $"[MediatR] Handling {requestName}. Request data: {JsonSerializer.Serialize(request)}.";
            _logger.LogInformation(logText);
            await _userLogRepository.AddAsync(UserLog.Create(userId, logText));


            var stopwatch = Stopwatch.StartNew();

            try
            {
                var response = await next();

                stopwatch.Stop();

                logText = $"[MediatR] Handled {requestName} in {stopwatch.ElapsedMilliseconds}ms.";
                _logger.LogInformation(logText);
                await _userLogRepository.AddAsync(UserLog.Create(userId, logText));

                return response;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();

                logText = $"[MediatR] Error handling {requestName} after {stopwatch.ElapsedMilliseconds}ms. Error message: {ex.Message}.";
                _logger.LogError(logText);
                await _userLogRepository.AddAsync(UserLog.Create(userId, logText));

                throw;
            }
        }
    }
}
