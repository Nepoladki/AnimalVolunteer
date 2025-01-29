using AnimalVolunteer.Core.Abstractions.CQRS;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Queries.GetRequestByUserId;

public record GetRequestByUserIdQuery(Guid UserId) : IQuery;

