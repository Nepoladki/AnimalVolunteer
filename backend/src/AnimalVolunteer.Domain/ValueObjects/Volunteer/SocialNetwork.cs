using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Domain.ValueObjects.Volunteer;

public record SocialNetwork
{
    private SocialNetwork() { }
    private SocialNetwork(string name, string url)
    {
        Name = name;
        URL = url;
    }
    public string Name { get; } = null!;
    public string URL { get; } = null!;
    public static Result<SocialNetwork, Error> Create(string name, string url)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > Constants.TEXT_LENGTH_LIMIT_LOW)
            return Errors.General.InvalidValue(nameof(name));

        if (string.IsNullOrWhiteSpace(url) || url.Length > Constants.TEXT_LENGTH_LIMIT_MEDIUM)
            return Errors.General.InvalidValue(nameof(url));

        return new SocialNetwork(name, url);
    }
}