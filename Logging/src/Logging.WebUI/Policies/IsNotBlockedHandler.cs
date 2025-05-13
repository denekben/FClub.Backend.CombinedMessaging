using FClub.Backend.Common.Exceptions;
using FClub.Backend.Common.Services;
using Logging.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Logging.WebUI.Policies
{
    public class IsNotBlockedHandler : AuthorizationHandler<IsNotBlockedRequirement>
    {
        private readonly IHttpContextService _httpContextService;
        private readonly IUserRepository _userRepository;

        public IsNotBlockedHandler(IHttpContextService httpContextService, IUserRepository userRepository)
        {
            _httpContextService = httpContextService;
            _userRepository = userRepository;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, IsNotBlockedRequirement requirement)
        {
            var userId = _httpContextService.GetCurrentUserId()
                ?? throw new BadRequestException("Invalid authorization header");
            var result = await _userRepository.IsBlockedAsync((Guid)userId);
            if (result != null && !(bool)result)
            {
                context.Succeed(requirement);
            }
        }
    }
}
