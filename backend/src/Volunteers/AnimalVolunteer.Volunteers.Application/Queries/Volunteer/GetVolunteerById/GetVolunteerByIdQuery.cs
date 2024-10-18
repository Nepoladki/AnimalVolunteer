using AnimalVolunteer.Core.Abstractions.CQRS;

namespace AnimalVolunteer.Volunteers.Application.Queries.Volunteer.GetVolunteerById;

public record GetVolunteerByIdQuery(Guid Id) : IQuery;
