namespace AnimalVolunteer.Accounts.Infrastructure.Options;

public class AdminOptions
{
    public const string SECTION_NAME = "AdminOptions";

    public string Username { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}