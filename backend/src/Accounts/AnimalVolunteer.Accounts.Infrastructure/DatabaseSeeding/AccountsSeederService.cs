﻿using AnimalVolunteer.Accounts.Domain.Models;
using AnimalVolunteer.Accounts.Domain.Models.AccountTypes;
using AnimalVolunteer.Accounts.Infrastructure.IdentitiyManagers;
using AnimalVolunteer.Accounts.Infrastructure.Options;
using AnimalVolunteer.Framework;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace AnimalVolunteer.Accounts.Infrastructure.DatabaseSeeding;

public class AccountsSeederService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly PermissonManager _permissionManager;
    private readonly RolePermissionManager _rolePermissionManager;
    private readonly AdminAccountManager _adminAccountManager;
    private readonly AdminOptions _adminOptions;
    private readonly ILogger<AccountsSeederService> _logger;

    public AccountsSeederService(
        PermissonManager permissionManager,
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        RolePermissionManager rolePermissionManager,
        IOptions<AdminOptions> adminOptions,
        ILogger<AccountsSeederService> logger,
        AdminAccountManager adminAccountManager)
    {
        _permissionManager = permissionManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _rolePermissionManager = rolePermissionManager;
        _adminOptions = adminOptions.Value;
        _logger = logger;
        _adminAccountManager = adminAccountManager;
    }

    public async Task SeedAsync()
    {

        _logger.LogInformation("Seeding accounts...");

        var json = await File.ReadAllTextAsync(JsonPaths.Permissions);

        var seedData = JsonSerializer.Deserialize<RolePermissionOptions>(json)
            ?? throw new ApplicationException($"Error occured while deserializing {JsonPaths.Permissions}");

        await SeedRoles(seedData);
        await SeedPermissions(seedData);
        await SeedRolesPermissions(seedData);
        await SeedAdmin();
    }

    private async Task SeedAdmin()
    {
        var adminRole = await _roleManager.FindByNameAsync(AdminAccount.ADMIN_ACCOUNT_NAME)
                    ?? throw new ApplicationException("Seeding error: unable to find admin role");

        var adminUser = User.CreateAdmin(_adminOptions.Username, _adminOptions.Email, adminRole);
        await _userManager.CreateAsync(adminUser, _adminOptions.Password);

        var adminAccount = new AdminAccount(adminUser);
        await _adminAccountManager.AddAdminAccount(adminAccount);
    }

    private async Task SeedRolesPermissions(RolePermissionOptions seedData)
    {
        foreach (var roleName in seedData.Roles.Keys)
        {
            var role = await _roleManager.FindByNameAsync(roleName);

            var rolePermissions = seedData.Roles[roleName];

            await _rolePermissionManager.AddRolesPermissionsIfNotExists(role!.Id, rolePermissions);

            _logger.LogInformation("Succsessfulley seeded roles-permissions relations to the database");
        }
    }

    private async Task SeedPermissions(RolePermissionOptions seedData)
    {
        var permissionsToAdd = seedData.Permissions.SelectMany(permissionGroup => permissionGroup.Value);

        await _permissionManager.AddPermissionsIfNotExists(permissionsToAdd);

        _logger.LogInformation("Succsessfully seeded permissions to the database");
    }

    private async Task SeedRoles(RolePermissionOptions seedData)
    {
        foreach (var role in seedData.Roles.Keys)
        {
            var existingRole = await _roleManager.FindByNameAsync(role);
            if (existingRole is null)
                await _roleManager.CreateAsync(new Role { Name = role });

            _logger.LogInformation("Succsessfully seeded roles to the database");
        }
    }
}