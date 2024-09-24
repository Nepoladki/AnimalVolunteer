namespace AnimalVolunteer.API.Controllers.Volunteer.Requests;

public record UpdateMainInfoRequest(
    string FirstName,
    string SurName,
    string LastName,
    string Email,
    string Description);