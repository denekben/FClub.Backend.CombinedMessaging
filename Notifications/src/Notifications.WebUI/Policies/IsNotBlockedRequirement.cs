using Microsoft.AspNetCore.Authorization;

namespace Notifications.WebUI.Policies
{
    public class IsNotBlockedRequirement : IAuthorizationRequirement
    {
        public bool IsBlocked { get; set; }
    }
}
