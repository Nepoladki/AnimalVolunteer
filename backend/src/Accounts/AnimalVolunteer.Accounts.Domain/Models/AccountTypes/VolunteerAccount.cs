using AnimalVolunteer.Accounts.Domain.Models.ValueObjects;

namespace AnimalVolunteer.Accounts.Domain.Models.AccountTypes;

public sealed class VolunteerAccount
{
    public const string VOLUNTEER_ACCOUNT_NAME = "Volunteer";

    // EF Core ctor
    private VolunteerAccount() { }
    public Guid Id { get; set; }
    public User User { get; set; }
    public Guid UserId { get; set; }
    public int Expirience { get; set; }
    public List<Certificate> Certificates { get; set; } = null!;
    public List<PaymentDetails> PaymentDetails { get; set; } = null!;
}
