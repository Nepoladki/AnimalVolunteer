using AnimalVolunteer.Accounts.Domain.Models.ValueObjects;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Accounts.Domain.Models.AccountTypes;

public sealed class VolunteerAccount
{
    public const string VOLUNTEER_ACCOUNT_NAME = "Volunteer";

    private List<PaymentDetails> _paymentDetails = [];

    private List<Certificate> _certificates = [];

    // EF Core ctor
    private VolunteerAccount() { }

    public VolunteerAccount(User user) => User = user;

    public Guid Id { get; set; }

    public User User { get; set; } = null!;

    public Guid UserId { get; set; }

    public int? Expirience { get; set; }

    public IReadOnlyList<Certificate> Certificates => _certificates;

    public IReadOnlyList<PaymentDetails> PaymentDetails => _paymentDetails;

    public void UpdatePaymentDetails(List<PaymentDetails> newPaymentDetails)
    {
        _paymentDetails = newPaymentDetails;
    }
}
