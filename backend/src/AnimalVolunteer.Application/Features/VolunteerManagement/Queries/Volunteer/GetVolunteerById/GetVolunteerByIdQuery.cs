using AnimalVolunteer.Application.Interfaces;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Queries.Volunteer.GetVolunteerById;

public record GetVolunteerByIdQuery(Guid Id) : IQuery;
