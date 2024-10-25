using AnimalVolunteer.Accounts.Contracts;
using AnimalVolunteer.Accounts.Infrastructure.IdentitiyManagers;

namespace AnimalVolunteer.Accounts.Web;

public class AccountsContract : IAccountsContract
{
    private readonly PermissonManager _permissionManager;

    public AccountsContract(PermissonManager permissionManager)
    {
        _permissionManager = permissionManager;
    }

    public async Task<IEnumerable<string>?> GetUserPermissions(
        Guid userId, CancellationToken cancellationToken = default)
    {
        return await _permissionManager.GetUserPermissionsById(userId, cancellationToken);
    }
}
