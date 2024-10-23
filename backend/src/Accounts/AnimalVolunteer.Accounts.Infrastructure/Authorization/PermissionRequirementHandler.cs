using Microsoft.AspNetCore.Authorization;

namespace AnimalVolunteer.Accounts.Infrastructure.Authorization
{
    public class PermissionRequirementHandler : AuthorizationHandler<PermissionAttribute>
    {
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context, PermissionAttribute requirement)
        {
            context.Succeed(requirement);
        }
    }

}
