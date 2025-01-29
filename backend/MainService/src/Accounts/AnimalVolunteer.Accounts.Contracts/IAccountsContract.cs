namespace AnimalVolunteer.Accounts.Contracts;

public interface IAccountsContract
{
    Task<IEnumerable<string>?> GetUserPermissions(Guid userId, CancellationToken cancellationToken = default);
}
