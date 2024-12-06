using AnimalVolunteer.Core.Abstractions.CQRS;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Queries.GetRequestsForConsideration;

public record GetRequestsForConsiderationQuery(int Page, int PageSize) : IQuery;

