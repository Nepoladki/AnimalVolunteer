namespace AnimalVolunteer.Domain.ValueObjects;

public record ContactInfoList
{
    public List<ContactInfo> Contacts { get; private set; } = null!;
}