using AnimalVolunteer.Core.DTOs.VolunteerRequests;

namespace AnimalVolunteer.VolunteerRequests.Web.Requests;

public record CreateRequestRequest(Guid UserId, VolunteerInfoDto VolunteerInfo);
