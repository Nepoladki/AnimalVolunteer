using AnimalVolunteer.Accounts.Domain.Models.Users;

namespace AnimalVolunteer.Accounts.Application.Interfaces;

public interface IJwtTokenProvider
{
     string GenerateAccessToken(User user, CancellationToken cancellationToken);
}
