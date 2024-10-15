using AnimalVolunteer.Application.Interfaces;

namespace AnimalVolunteer.Volunteers.Application.Queries.Pet.GetPetById;

public record GetPetByIdQuery(Guid Id) : IQuery;
