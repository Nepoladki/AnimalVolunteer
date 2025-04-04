namespace AnimalVolunteer.Accounts.Infrastructure.DatabaseSeeding;

public class RolePermissionToSeed
{
    public Dictionary<string, string[]> Permissions { get; set; } = [];
    public Dictionary<string, string[]> Roles { get; set; } = [];
}
