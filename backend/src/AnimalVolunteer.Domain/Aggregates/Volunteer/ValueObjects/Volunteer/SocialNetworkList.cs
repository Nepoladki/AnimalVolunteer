using System.Runtime.InteropServices;

namespace AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Volunteer;

public record SocialNetworkList
{
    private SocialNetworkList() { }
    private SocialNetworkList(IEnumerable<SocialNetwork> list) => SocialNetworks = list.ToList();
    public IReadOnlyList<SocialNetwork> SocialNetworks { get; } = null!;
    public static SocialNetworkList Create(IEnumerable<SocialNetwork> list) => new(list);
}