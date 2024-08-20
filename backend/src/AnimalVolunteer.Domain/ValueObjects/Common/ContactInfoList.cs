namespace AnimalVolunteer.Domain.ValueObjects.Common;

public record ContactInfoList
{
    private ContactInfoList() {}
    private ContactInfoList(List<ContactInfo> list) { Contacts = list; }
    public List<ContactInfo> Contacts { get; private set; } = null!;
    public static ContactInfoList Create(List<ContactInfo> list) => new(list);
}