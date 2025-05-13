using FClub.Backend.Common.Logging;
using FClub.Backend.Common.RabbitMQMessaging.Publisher;
using FClub.Backend.Common.Services;
using Management.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;

namespace Management.Infrastructure.Logging
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        private static readonly ConcurrentDictionary<Type, bool> _skipLoggingCache = new();
        private readonly IHttpContextService _contextService;
        private readonly IMessageBusPublisher _publisher;

        public LoggingBehavior(
            ILogger<LoggingBehavior<TRequest, TResponse>> logger,
            IHttpContextService currentUserService,
            IMessageBusPublisher publisher)
        {
            _logger = logger;
            _contextService = currentUserService;
            _publisher = publisher;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            await _publisher.InitializeAsync();

            var shouldSkip = _skipLoggingCache.GetOrAdd(typeof(TRequest), type =>
            {
                var expectedHandlerInterface = typeof(IRequestHandler<,>)
                    .MakeGenericType(type, typeof(TResponse));

                var handlerTypes = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a => a.GetTypes())
                    .Where(t => expectedHandlerInterface.IsAssignableFrom(t));

                return handlerTypes.Any(t => t.GetCustomAttribute<SkipLoggingAttribute>() != null);
            });

            if (shouldSkip)
            {
                return await next();
            }

            var requestName = typeof(TRequest).Name;
            Guid? userId = null;
            try
            {
                userId = _contextService.GetCurrentUserId();
            }
            catch (Exception)
            {
            }

            if (userId == null || userId == Guid.Empty)
            {
                return await next();
            }

            var logText = $"[MediatR] Handling {requestName}. Request data: {JsonSerializer.Serialize(request)}.";
            _logger.LogInformation(logText);
            var log = UserLog.Create((Guid)userId, logText);
            await _publisher.PublishAsync(new UserLogAdded(log.Id, log.AppUserId, log.ServiceName, log.Text, log.CreatedDate));

            var stopwatch = Stopwatch.StartNew();

            try
            {
                var response = await next();

                stopwatch.Stop();

                logText = $"[MediatR] Handled {requestName} in {stopwatch.ElapsedMilliseconds}ms.";
                _logger.LogInformation(logText);
                log = UserLog.Create((Guid)userId, logText);
                await _publisher.PublishAsync(new UserLogAdded(log.Id, log.AppUserId, log.ServiceName, log.Text, log.CreatedDate));

                return response;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();

                logText = $"[MediatR] Error handling {requestName} after {stopwatch.ElapsedMilliseconds}ms. Error message: {ex.Message}.";
                _logger.LogError(logText);
                log = UserLog.Create((Guid)userId, logText);
                await _publisher.PublishAsync(new UserLogAdded(log.Id, log.AppUserId, log.ServiceName, log.Text, log.CreatedDate));

                throw;
            }
        }
    }

    public sealed record UserLogAdded(
        Guid Id,
        Guid AppUserId,
        string ServiceName,
        string Text,
        DateTime CreatedDate
    ) : IMessage;
}
