namespace AnimalVolunteer.Domain.Entities;

public class SocialNetwork
{
    public Guid SocialNetworkId { get; set; }
    public string Name { get; private set; } = null!;
    public string URL { get; private set; } = null!;
}