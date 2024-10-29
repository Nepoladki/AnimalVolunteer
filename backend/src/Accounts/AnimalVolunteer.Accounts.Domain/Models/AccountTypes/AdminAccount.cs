using System.Runtime.CompilerServices;

namespace AnimalVolunteer.Accounts.Domain.Models.AccountTypes;

public class AdminAccount
{
    public const string ADMIN_ACCOUNT_NAME = nameof(AdminAccount);
   
    //Ef core ctor
    private AdminAccount() { }

    public AdminAccount(User user)
    {
        Id = Guid.NewGuid();
        User = user;
    }

    public Guid Id { get; set; }

    public User User { get; set; }

    public Guid UserId { get; set; }
}
