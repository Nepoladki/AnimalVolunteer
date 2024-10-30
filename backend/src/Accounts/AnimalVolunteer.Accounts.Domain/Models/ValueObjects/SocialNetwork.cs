using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Accounts.Domain.Models.ValueObjects;

public class SocialNetwork
{
    public string Name { get; set; } = string.Empty;
    public string URL { get; set; } = string.Empty;
}
