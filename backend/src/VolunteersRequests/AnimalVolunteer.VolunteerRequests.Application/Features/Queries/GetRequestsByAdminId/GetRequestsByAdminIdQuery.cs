using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.VolunteerRequests.Domain.Enums;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Queries.GetRequestsByAdminId;

public record GetRequestsByAdminIdQuery(
    Guid AdminId, 
    int Page, 
    int PageSize,
    VolunteerRequestStatus? Status) : IQuery;

