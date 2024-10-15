namespace AnimalVolunteer.Core.Options;

public class DatabaseOptions
{
    public const string SECTION_NAME = "DatabaseOptions";
    public string PostgresConnectionName { get; init; } = string.Empty;
}
