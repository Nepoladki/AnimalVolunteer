namespace AnimalVolunteer.Accounts.Application.Models;

public record JwtResult(string AccessToken, Guid Jti);
