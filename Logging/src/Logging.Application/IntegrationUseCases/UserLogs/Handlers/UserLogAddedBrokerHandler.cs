using FClub.Backend.Common.RabbitMQMessaging.Publisher;
using FClub.Backend.Common.RabbitMQMessaging.Subscriber;
using Logging.Domain.Entities;
using Logging.Domain.Repositories;
using StackExchange.Redis;

public class UserLogAddedBrokerHandler : IMessageHandler<UserLogAdded>
{
    private readonly IRepository _repository;
    private readonly IUserLogRepository _userLogRepository;
    private readonly IConnectionMultiplexer _redis;

    public UserLogAddedBrokerHandler(
        IRepository repository,
        IUserLogRepository userLogRepository,
        IConnectionMultiplexer redis)
    {
        _repository = repository;
        _userLogRepository = userLogRepository;
        _redis = redis;
    }

    public async Task HandleAsync(UserLogAdded @event, CancellationToken cancellationToken = default)
    {
        var (id, appUserId, serviceName, text, createdDate) = @event;

        await _userLogRepository.AddAsync(
            UserLog.Create(id, appUserId, serviceName, text, createdDate));

        await InvalidateUserLogsCache(appUserId);

        await _repository.SaveChangesAsync();
    }

    private async Task InvalidateUserLogsCache(Guid appUserId)
    {
        var db = _redis.GetDatabase();
        var server = _redis.GetServer(_redis.GetEndPoints().First());

        var patterns = new[]
        {
            $"*current_logs:{appUserId}*",
            $"*logs:{appUserId}*"
        };

        foreach (var pattern in patterns)
        {
            await foreach (var key in server.KeysAsync(pattern: pattern))
            {
                await db.KeyDeleteAsync(key);
            }
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