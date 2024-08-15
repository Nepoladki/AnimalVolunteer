namespace AnimalVolunteer.Domain.ValueObjects;

public record SocialNetworkList
{
    public List<SocialNetwork> SocialNetworks { get; private set; } = null!;
}