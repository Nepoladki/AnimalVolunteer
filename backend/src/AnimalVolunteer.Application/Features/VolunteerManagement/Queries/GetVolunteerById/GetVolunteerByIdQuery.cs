using AnimalVolunteer.Application.Interfaces;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Queries.GetVolunteerById;

public record GetVolunteerByIdQuery(Guid Id) : IQuery;
