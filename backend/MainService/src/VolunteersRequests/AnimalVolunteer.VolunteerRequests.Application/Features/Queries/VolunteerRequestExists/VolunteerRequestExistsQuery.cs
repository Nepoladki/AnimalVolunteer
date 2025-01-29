using AnimalVolunteer.Core.Abstractions.CQRS;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Queries.VolunteerRequestExists;

public record VolunteerRequestExistsQuery(Guid UserId) : IQuery; 

