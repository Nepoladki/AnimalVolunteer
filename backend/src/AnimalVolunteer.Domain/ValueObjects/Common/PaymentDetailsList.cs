namespace AnimalVolunteer.Domain.ValueObjects.Common;

public record PaymentDetailsList
{
    public List<PaymentDetails> Payments { get; private set; } = null!;
}