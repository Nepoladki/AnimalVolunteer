using Microsoft.EntityFrameworkCore.Metadata;

namespace AnimalVolunteer.Accounts.Domain.Models;

public sealed class Permission
{
    public Guid Id { get; set; }
    public string CodeName { get; set; } = string.Empty;
}
