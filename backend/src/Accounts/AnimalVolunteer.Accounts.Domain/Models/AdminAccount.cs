namespace AnimalVolunteer.Accounts.Domain.Models;

public class AdminAccount
{
    public Guid UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
}
