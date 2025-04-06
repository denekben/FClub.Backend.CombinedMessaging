namespace AccessControl.Application.Services
{
    public interface INotificationService
    {
        Task ClientEntered(Guid branchId, uint entriesQuantity = 1);
    }
}
