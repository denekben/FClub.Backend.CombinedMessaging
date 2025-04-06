﻿using Microsoft.AspNetCore.SignalR;

namespace AccessControl.Infrastructure.Hubs
{
    public sealed class AccessControlServiceHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}
