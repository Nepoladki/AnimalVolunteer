using AnimalVolunteer.Accounts.Domain.Models;

namespace AnimalVolunteer.Accounts.Application.Interfaces;

public interface IRolesPermissionsRepository
{
    Task<Permission?> GetPermissionByCode(string code);

    Task<IEnumerable<Permission>?> GetAllPermissions(CancellationToken cancellationToken = default);

    Task<IEnumerable<string>> GetAllExistingPermissionsCodes(CancellationToken cancellationToken = default);

    Task AddRange(IEnumerable<Permission> permissions, CancellationToken cancellationToken = default);

    Task<HashSet<string>> GetPermissionCodesByUserId(Guid userId, CancellationToken cancellationToken = default);

    Task AddRolesWithPermissions(IEnumerable<Role> rolesWithPermissions, CancellationToken cancellationToken = default);

    Task ClearRolesAndPermissions(CancellationToken cancellationToken = default);

    Task<Role?> GetRoleByName(string name, CancellationToken cancellationToken = default);
}
