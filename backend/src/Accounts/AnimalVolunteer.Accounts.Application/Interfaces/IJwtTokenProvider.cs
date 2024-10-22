using AnimalVolunteer.Accounts.Domain.Models;

namespace AnimalVolunteer.Accounts.Application.Interfaces;

public interface IJwtTokenProvider
{
     string GenerateAccessToken(User user, CancellationToken cancellationToken);
}
