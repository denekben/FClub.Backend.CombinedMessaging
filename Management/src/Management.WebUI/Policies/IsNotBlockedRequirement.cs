using Microsoft.AspNetCore.Authorization;

namespace Management.WebUI.Policies
{
    public class IsNotBlockedRequirement : IAuthorizationRequirement
    {
        public bool IsBlocked { get; set; }
    }
}
