using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Domain.ValueObjects;

public record SocialNetwork
{
    // EF Core ctor
    private SocialNetwork() { }
    private SocialNetwork(string name, string url)
    {
        Name = name;
        URL = url;
    }
    public string Name { get; } = null!;
    public string URL { get; } = null!;
    public static Result<SocialNetwork> Create(string name, string url)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > Constants.TEXT_LENGTH_LIMIT_LOW)
            return Result.Failure<SocialNetwork>("Invalid name");

        if (string.IsNullOrWhiteSpace(url) || url.Length > Constants.TEXT_LENGTH_LIMIT_MEDIUM)
            return Result.Failure<SocialNetwork>("Invalid URL");

        return Result.Success(new SocialNetwork(name, url));
    }
}