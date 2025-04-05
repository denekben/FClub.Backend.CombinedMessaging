using FClub.Backend.Common.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using Notifications.Domain.Entities;
using Notifications.Domain.Repositories;
using System.Diagnostics;
using System.Text.Json;

namespace Management.Infrastructure.Logging
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        private readonly IHttpContextService _contextService;
        private readonly IUserLogRepository _userLogRepository;
        private readonly IRepository _repository;

        public LoggingBehavior(
            ILogger<LoggingBehavior<TRequest, TResponse>> logger,
            IHttpContextService currentUserService,
            IRepository repository,
            IUserLogRepository userLogRepository)
        {
            _logger = logger;
            _contextService = currentUserService;
            _repository = repository;
            _userLogRepository = userLogRepository;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _contextService.GetCurrentUserId() ?? Guid.Empty;

            var logText = $"[MediatR] Handling {requestName}. Request data: {JsonSerializer.Serialize(request)}.";
            _logger.LogInformation(logText);
            await _userLogRepository.AddAsync(UserLog.Create(userId, logText));
            await _repository.SaveChangesAsync();

            var stopwatch = Stopwatch.StartNew();

            try
            {
                var response = await next();

                stopwatch.Stop();

                logText = $"[MediatR] Handled {requestName} in {stopwatch.ElapsedMilliseconds}ms.";
                _logger.LogInformation(logText);
                await _userLogRepository.AddAsync(UserLog.Create(userId, logText));
                await _repository.SaveChangesAsync();

                return response;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();

                logText = $"[MediatR] Error handling {requestName} after {stopwatch.ElapsedMilliseconds}ms. Error message: {ex.Message}.";
                _logger.LogError(logText);
                await _userLogRepository.AddAsync(UserLog.Create(userId, logText));
                await _repository.SaveChangesAsync();

                throw;
            }
        }
    }
}
