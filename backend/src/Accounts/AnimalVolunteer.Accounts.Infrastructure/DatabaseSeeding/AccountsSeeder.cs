using AnimalVolunteer.Accounts.Domain.Models;
using AnimalVolunteer.Framework;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AnimalVolunteer.Accounts.Infrastructure.DatabaseSeeding;

public class AccountsSeeder
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<AccountsSeeder> _logger;

    public AccountsSeeder(
        IServiceScopeFactory scopeFactory, ILogger<AccountsSeeder> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        _logger.LogInformation("Seeding accounts permissions...");

        using var scope = _scopeFactory.CreateScope();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
        var permissionManager = scope.ServiceProvider.GetRequiredService<PermissonManager>();
        var rolePermissionManager = scope.ServiceProvider.GetRequiredService<RolePermissonManager>();

        var json = await File.ReadAllTextAsync(JsonPaths.Permissions);

        var seedData = JsonSerializer.Deserialize<RolePermissionConfig>(json)
            ?? throw new ApplicationException($"Error occured while deserializing {JsonPaths.Permissions}");

        await SeedPermissions(permissionManager, seedData);
        await SeedRoles(roleManager, seedData);
        await SeedRolesPermissions(roleManager, rolePermissionManager, seedData);
    }

    private async Task SeedRolesPermissions(
        RoleManager<Role> roleManager, 
        RolePermissonManager rolePermissionManager, 
        RolePermissionConfig seedData)
    {
        foreach (var roleName in seedData.Roles.Keys)
        {
            var role = await roleManager.FindByNameAsync(roleName);

            var rolePermissions = seedData.Roles[roleName];

            await rolePermissionManager.AddRolesPermissionsIfNotExists(role!.Id, rolePermissions);

            _logger.LogInformation("Succsessfulley seeded roles-permissions relations to the database");
        }
    }

    private async Task SeedPermissions(PermissonManager permissionManager, RolePermissionConfig seedData)
    {
        var permissionsToAdd = seedData.Permissions.SelectMany(permissionGroup => permissionGroup.Value);

        await permissionManager.AddPermissionsIfNotExists(permissionsToAdd);

        _logger.LogInformation("Succsessfully seeded permissions to the database");
    }

    private async Task SeedRoles(RoleManager<Role> roleManager, RolePermissionConfig seedData)
    {
        foreach(var role in seedData.Roles.Keys)
        {
            var existingRole = await roleManager.FindByNameAsync(role);
            if (existingRole is null)
                await roleManager.CreateAsync(new Role { Name = role });

            _logger.LogInformation("Succsessfully seeded roles to the database");
        }
    }
}

public class RolePermissionConfig
{
    public Dictionary<string, string[]> Permissions { get; set; } = [];
    public Dictionary<string, string[]> Roles { get; set; } = [];
}
