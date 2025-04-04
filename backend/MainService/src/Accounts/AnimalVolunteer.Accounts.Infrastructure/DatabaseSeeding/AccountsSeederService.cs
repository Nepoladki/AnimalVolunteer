using AnimalVolunteer.Accounts.Application.Interfaces;
using AnimalVolunteer.Accounts.Domain.Models;
using AnimalVolunteer.Accounts.Domain.Models.AccountTypes;
using AnimalVolunteer.Accounts.Infrastructure.Options;
using AnimalVolunteer.Accounts.Infrastructure.Repositories;
using AnimalVolunteer.Core;
using AnimalVolunteer.Core.Abstractions;
using AnimalVolunteer.Framework;
using AnimalVolunteer.SharedKernel.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace AnimalVolunteer.Accounts.Infrastructure.DatabaseSeeding;

public class AccountsSeederService(
    UserManager<User> userManager,
    IAccountsRepository accountsRepository,
    IRolesPermissionsRepository rolesPermissionsRepository,
    IOptions<AdminOptions> adminOptions,
    ILogger<AccountsSeederService> logger,
    [FromKeyedServices(Modules.Accounts)]IUnitOfWork unitOfWork)
{
    private readonly AdminOptions _adminOptions = adminOptions.Value;
    public async Task SeedAsync()
    {

        logger.LogInformation("Seeding accounts...");

        var json = await File.ReadAllTextAsync(JsonPaths.Permissions);

        var seedData = JsonSerializer.Deserialize<RolePermissionToSeed>(json)
            ?? throw new ApplicationException($"Error occured while deserializing {JsonPaths.Permissions}");

        await rolesPermissionsRepository.ClearRolesAndPermissions();

        await SeedPermissions(seedData.Permissions);

        await SeedRolesPermissionsRelationship(seedData.Roles);

        await SeedAdminAccount();
    }

    private async Task SeedPermissions(Dictionary<string, string[]> permissions)
    {
        var permissionEntities = permissions.SelectMany(x => x.Value.Select(y => new Permission { CodeName = y }));
        await rolesPermissionsRepository.AddRange(permissionEntities);
    }

    private async Task SeedRolesPermissionsRelationship(
        Dictionary<string, string[]> roles, CancellationToken cancellationToken = default)
    {
        var existingPermissions = await rolesPermissionsRepository.GetAllPermissions(cancellationToken);
        if (existingPermissions is null)
            throw new ApplicationException("Could not find permissions in database");

        List<Role> rolesEntities = [];

        foreach (var role in roles)
        {
            Role roleEntity = new() { Name = role.Key };

            foreach (var permission in role.Value)
            {
                Permission permissionEntity = existingPermissions.First(x => x.CodeName == permission);
                roleEntity.Permissions.Add(permissionEntity);
            }

            rolesEntities.Add(roleEntity);
        }

        await rolesPermissionsRepository.AddRolesWithPermissions(rolesEntities, cancellationToken);
    }

    private async Task SeedAdminAccount()
    {
        var adminExists = await userManager.Users
            .FirstOrDefaultAsync(u => u.Email == _adminOptions.Email);

        if (adminExists is not null)
            return;

        var adminRole = await rolesPermissionsRepository.GetRoleByName(AdminAccount.ADMIN_ACCOUNT_NAME)
                        ?? throw new ApplicationException("Could not find admin role.");

        using var transaction = await unitOfWork.BeginTransaction();

        try
        {
            var fullName = FullName.Create(_adminOptions.Username, _adminOptions.Username, _adminOptions.Username)
                .Value;

            var adminUser = User.CreateAdmin(
                _adminOptions.Username,
                _adminOptions.Email,
                fullName,
                adminRole);

            if (adminUser.IsFailure)
                throw new ApplicationException(adminUser.Error.Message);

            await userManager.CreateAsync(adminUser.Value, _adminOptions.Password);

            var adminAccount = adminUser.Value.CreateAdminAccount();

            await accountsRepository.AddAdminAccount(adminAccount);

            await unitOfWork.SaveChanges();

            transaction.Commit();

            logger.LogInformation("Admin account added to database");
        }
        catch (Exception ex)
        {
            logger.LogError("Creating admin was failed");

            transaction.Rollback();

            throw new ApplicationException(ex.Message);
        }
    }
}
