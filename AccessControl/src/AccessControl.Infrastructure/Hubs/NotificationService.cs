using AccessControl.Application.Services;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace AccessControl.Infrastructure.Hubs
{
    public sealed class NotificationService : IWSNotificationService
    {
        private readonly IHubContext<AccessControlServiceHub> _hubContext;

        public NotificationService(IHubContext<AccessControlServiceHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task ClientEntered(Guid branchId, int entriesQuantity = 1)
        {
            var json = JsonSerializer.Serialize(new { BranchId = branchId, EntriesQuantity = entriesQuantity });
            await _hubContext.Clients.All.SendAsync(json);
        }

        public async Task ClientExited(Guid branchId, int entriesQuantity = -1)
        {
            var json = JsonSerializer.Serialize(new { BranchId = branchId, EntriesQuantity = entriesQuantity });
            await _hubContext.Clients.All.SendAsync(json);
        }
    }
}
