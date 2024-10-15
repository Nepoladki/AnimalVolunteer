using AnimalVolunteer.Application.Interfaces;

namespace AnimalVolunteer.Volunteers.Application.Queries.Volunteer.GetVolunteerById;

public record GetVolunteerByIdQuery(Guid Id) : IQuery;
