using Microsoft.EntityFrameworkCore;

namespace AnimalVolunteer.Accounts.Domain.Models.AccountTypes;

public sealed class ParticipantAccount
{
    public const string PARTICIPANT_ACCOUNT_NAME = "Participant";

    // EF Core ctor
    private ParticipantAccount() { }
    public Guid Id { get; set; }
    public User User { get; set; }
    public Guid UserId { get; set; }

    public static ParticipantAccount Create(User user)
    {
        return new ParticipantAccount
        {
            User = user,
            UserId = user.Id
        };
    }
}
