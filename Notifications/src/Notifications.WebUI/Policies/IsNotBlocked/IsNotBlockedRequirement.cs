using Microsoft.AspNetCore.Authorization;

namespace Notifications.WebUI.Policies.IsNotBlocked
{
    public class IsNotBlockedRequirement : IAuthorizationRequirement
    {
        public bool IsBlocked { get; set; }
    }
}
