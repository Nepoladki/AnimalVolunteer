using AnimalVolunteer.Core.Abstractions.CQRS;

namespace AnimalVolunteer.Volunteers.Application.Queries.Pet.GetPetById;

public record GetPetByIdQuery(Guid Id) : IQuery;
