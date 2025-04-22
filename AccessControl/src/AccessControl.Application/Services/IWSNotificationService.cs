namespace AccessControl.Application.Services
{
    public interface IWSNotificationService
    {
        Task ClientEntered(Guid branchId, int entriesQuantity = 1);
        Task ClientExited(Guid branchId, int entriesQuantity = -1);
    }
}
