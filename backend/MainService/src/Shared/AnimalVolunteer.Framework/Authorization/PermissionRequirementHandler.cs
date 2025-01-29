using AnimalVolunteer.Accounts.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;

namespace AnimalVolunteer.Framework.Authorization;

public class PermissionRequirementHandler : AuthorizationHandler<PermissionAttribute>
{
    private readonly IServiceScopeFactory _scopeFactory;

    public PermissionRequirementHandler(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, PermissionAttribute requirement)
    {
        var userIdFromClaims = context.User.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.ID);
        if (userIdFromClaims is null)
        {
            context.Fail();
            return;
        }

        if (Guid.TryParse(userIdFromClaims.Value, out var userId) == false)
        {
            context.Fail();
            return;
        }

        using var scope = _scopeFactory.CreateScope();
        var accountsContract = scope.ServiceProvider.GetRequiredService<IAccountsContract>();

        var userPermissions = await accountsContract.GetUserPermissions(userId);
        if (userPermissions is null)
        {
            context.Fail();
            return;
        }

        if (userPermissions.Contains(requirement.Code))
            context.Succeed(requirement);

        return;
    }
}
