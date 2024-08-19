namespace AnimalVolunteer.Domain.ValueObjects.Common;

public record ContactInfoList
{
    public List<ContactInfo> Contacts { get; private set; } = null!;
}