using System.Runtime.CompilerServices;

namespace AnimalVolunteer.Accounts.Domain.Models.AccountTypes;

public sealed class AdminAccount
{
    public const string ADMIN_ACCOUNT_NAME = "Admin";
   
    // EF Core ctor
    private AdminAccount() { }

    public AdminAccount(User user)
        => User = user;

    public Guid Id { get; set; }

    public User User { get; set; } = null!;

    public Guid UserId { get; set; }
}
