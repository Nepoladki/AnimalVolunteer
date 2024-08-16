namespace AnimalVolunteer.Domain.ValueObjects;

public record PaymentDetailsList
{
    public List<PaymentDetails> Payments { get; private set; } = null!;
}