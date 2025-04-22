namespace Notifications.Domain.Repositories
{
    public interface IRepository
    {
        Task SaveChangesAsync();
        Task SaveLogsAsync();
    }
}
