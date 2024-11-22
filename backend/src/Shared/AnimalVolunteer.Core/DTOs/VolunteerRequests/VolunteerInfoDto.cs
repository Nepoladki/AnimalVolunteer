namespace AnimalVolunteer.Core.DTOs.VolunteerRequests;

public record VolunteerInfoDto
{
    public string ExpirienceDescription { get; set; } = string.Empty;
    public string Passport { get; set; } = string.Empty;
}

