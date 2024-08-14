namespace AnimalVolunteer.Domain.Entities;

public class PaymentDetails
{
    public Guid PaymentDetalisId { get; set; }
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
}