namespace AccessControl.Application.Services
{
    public interface IWSNotificationService
    {
        Task ClientEntered(Guid branchId, uint entriesQuantity = 1);
    }
}
