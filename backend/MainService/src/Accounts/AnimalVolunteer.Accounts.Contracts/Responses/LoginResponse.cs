namespace AnimalVolunteer.Accounts.Contracts.Responses;

public record LoginResponse(string AccessToken, Guid RefreshToken);

