namespace AnimalVolunteer.Domain.Common.ValueObjects;

public record ContactInfoList
{
    private ContactInfoList() { }
    private ContactInfoList(IEnumerable<ContactInfo> list) => Contacts = list.ToList();
    public IReadOnlyList<ContactInfo> Contacts { get; } = [];
    public static ContactInfoList Create(IEnumerable<ContactInfo> list) => new(list);
}