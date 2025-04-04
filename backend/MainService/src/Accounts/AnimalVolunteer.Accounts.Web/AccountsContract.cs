using AnimalVolunteer.Accounts.Application.Interfaces;
using AnimalVolunteer.Accounts.Contracts;

namespace AnimalVolunteer.Accounts.Web;

public class AccountsContract(IRolesPermissionsRepository repository) : IAccountsContract
{

    public async Task<IEnumerable<string>?> GetUserPermissions(
        Guid userId, CancellationToken cancellationToken = default)
    {
        return await repository.GetPermissionCodesByUserId(userId, cancellationToken);
    }
}
