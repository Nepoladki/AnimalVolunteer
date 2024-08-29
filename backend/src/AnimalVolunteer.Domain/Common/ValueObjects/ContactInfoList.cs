﻿namespace AnimalVolunteer.Domain.Common.ValueObjects;

public record ContactInfoList
{
    private ContactInfoList() { }
    private ContactInfoList(IEnumerable<ContactInfo> list) => Contacts = list.ToList();
    public IReadOnlyList<ContactInfo> Contacts { get; } = default!;
    public static ContactInfoList Create(IEnumerable<ContactInfo> list) => new(list);
    public static ContactInfoList CreateEmpty() => new([]);
}