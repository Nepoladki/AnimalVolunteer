namespace AnimalVolunteer.Accounts.Domain.Models;

public class RefreshSession
{
    public const string TABLE_NAME = "refresh_session";
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public User User  { get; init; } = default!;
    public Guid RefreshToken { get; init; }
    public Guid Jti {  get; init; }
    public DateTime ExpiresAt { get; init; }
    public DateTime CreatedAt { get; init; }

}

