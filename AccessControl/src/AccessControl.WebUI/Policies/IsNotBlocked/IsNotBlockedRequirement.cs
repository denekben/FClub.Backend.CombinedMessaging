using Microsoft.AspNetCore.Authorization;

namespace AccessControl.WebUI.Policies.IsNotBlocked
{
    public class IsNotBlockedRequirement : IAuthorizationRequirement
    {
        public bool IsBlocked { get; set; }
    }
}
