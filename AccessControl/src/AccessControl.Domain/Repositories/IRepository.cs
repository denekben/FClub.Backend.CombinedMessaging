namespace AccessControl.Domain.Repositories
{
    public interface IRepository
    {
        Task SaveChangesAsync();
        Task SaveLogsAsync();
    }
}
