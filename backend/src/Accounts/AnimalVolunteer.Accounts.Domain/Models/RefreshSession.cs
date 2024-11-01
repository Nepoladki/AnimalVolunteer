namespace AnimalVolunteer.Accounts.Domain.Models;

public class RefreshSession
{
    public const string TABLE_NAME = "refresh_session";
    // EF Core ctor
    private RefreshSession() { }
    public Guid Id { get; init; }
    public Guid UserId { get; set; }
    public User User  { get; set; } = default!;
    public Guid Token { get; init; }
    public DateTime ExpiresAt { get; init; }
    public DateTime CreatedAt { get; init; }

}

