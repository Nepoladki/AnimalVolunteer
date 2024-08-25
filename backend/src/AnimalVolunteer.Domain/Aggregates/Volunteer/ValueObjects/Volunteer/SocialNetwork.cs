using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Volunteer;

public record SocialNetwork
{
    public const int MAX_NAME_LENGTH = 25;
    public const int MAX_URL_LENGTH = 150;
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
        if (string.IsNullOrWhiteSpace(name) || name.Length > MAX_NAME_LENGTH)
            return Errors.General.InvalidValue(nameof(name));

        if (string.IsNullOrWhiteSpace(url) || url.Length > MAX_URL_LENGTH)
            return Errors.General.InvalidValue(nameof(url));

        return new SocialNetwork(name, url);
    }
}