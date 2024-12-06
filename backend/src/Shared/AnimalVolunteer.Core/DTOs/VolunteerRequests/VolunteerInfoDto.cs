using LinqToDB.Mapping;

namespace AnimalVolunteer.Core.DTOs.VolunteerRequests;

public record VolunteerInfoDto
{
    [Column("expirience_description")]
    public string ExpirienceDescription { get; set; } = string.Empty;

    [Column("passport")]
    public string Passport { get; set; } = string.Empty;
}

