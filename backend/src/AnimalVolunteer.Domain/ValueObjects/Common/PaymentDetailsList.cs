using System.Runtime.InteropServices;

namespace AnimalVolunteer.Domain.ValueObjects.Common;

public record PaymentDetailsList
{
    private PaymentDetailsList() {}
    private PaymentDetailsList(List<PaymentDetails> list) { Payments = list; }
    public List<PaymentDetails> Payments { get; private set; } = null!;
    public static PaymentDetailsList Create(List<PaymentDetails> list) => new(list);
}