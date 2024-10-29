using AnimalVolunteer.Accounts.Domain.Models.ValueObjects;

namespace AnimalVolunteer.Accounts.Domain.Models.AccountTypes;

public class VolunteerAccount
{
    public Guid UserId { get; set; }
    public int Expirience { get; set; }
    public List<Certificate> Certificates { get; set; } = null!;
    public List<PaymentDetails> PaymentDetails { get; set; } = null!;
}
