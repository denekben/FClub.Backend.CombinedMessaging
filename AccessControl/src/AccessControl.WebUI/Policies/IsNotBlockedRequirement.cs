using Microsoft.AspNetCore.Authorization;

namespace AccessControl.WebUI.Policies
{
    public class IsNotBlockedRequirement : IAuthorizationRequirement
    {
        public bool IsBlocked { get; set; }
    }
}
