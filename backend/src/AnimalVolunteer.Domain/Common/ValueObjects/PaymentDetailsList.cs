using System.Runtime.InteropServices;

namespace AnimalVolunteer.Domain.Common.ValueObjects;

public record PaymentDetailsList
{
    private PaymentDetailsList() { }
    private PaymentDetailsList(IEnumerable<PaymentDetails> list) => Payments = list.ToList();
    public IReadOnlyList<PaymentDetails> Payments { get; } = [];
    public static PaymentDetailsList Create(IEnumerable<PaymentDetails> list) => new(list);
}