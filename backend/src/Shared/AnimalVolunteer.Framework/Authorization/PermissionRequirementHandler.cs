using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalVolunteer.Framework.Authorization
{
    public class PermissionRequirementHandler : AuthorizationHandler<PermissionAttribute>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PermissionRequirementHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context, PermissionAttribute requirement)
        {



            context.Succeed(requirement);
        }
    }

}
