using Microsoft.AspNetCore.Authorization;

namespace Logging.WebUI.Policies
{
    public class IsNotBlockedRequirement : IAuthorizationRequirement
    {
        public bool IsBlocked { get; set; }
    }
}
