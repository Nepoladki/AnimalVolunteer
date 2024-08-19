namespace AnimalVolunteer.Domain.ValueObjects.Volunteer;

public record SocialNetworkList
{
    public List<SocialNetwork> SocialNetworks { get; private set; } = null!;
}