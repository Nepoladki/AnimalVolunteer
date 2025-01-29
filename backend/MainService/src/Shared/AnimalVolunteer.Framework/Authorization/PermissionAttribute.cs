using Microsoft.AspNetCore.Authorization;

namespace AnimalVolunteer.Framework.Authorization;

public class PermissionAttribute : AuthorizeAttribute, IAuthorizationRequirement
{
    public string Code { get; set; } = string.Empty;

    public PermissionAttribute(string code) : base(policy: code)
    {
        Code = code;
    }
}