namespace AnimalVolunteer.Accounts.Domain.Models.VolunteerAccount;

public class VolunteerAccount
{
    public Guid UserId { get; set; }
    public int Expirience { get; set; }
    public string FullName { get; set; } = string.Empty;
    public List<Certificate> Certificates { get; set; } = null!;
    public List<PaymentDetails> PaymentDetails { get; set; } = null!;
}
