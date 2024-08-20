using System.Runtime.InteropServices;

namespace AnimalVolunteer.Domain.ValueObjects.Volunteer;

public record SocialNetworkList
{
    private SocialNetworkList() {}
    private SocialNetworkList(List<SocialNetwork> list) { SocialNetworks = list; }
    public List<SocialNetwork> SocialNetworks { get; private set; } = null!;
    public static SocialNetworkList Create(List<SocialNetwork> list) => new(list);
}