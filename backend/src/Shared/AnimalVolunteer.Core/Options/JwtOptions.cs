namespace AnimalVolunteer.Core.Options;

public class JwtOptions
{
    public const string SECTION_NAME = nameof(JwtOptions);
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
    public string SecretKey { get; init; } = string.Empty;
    public int ClockSkewMinutes { get; init; }
    public int ExpiringMinutes { get; init; }
}
