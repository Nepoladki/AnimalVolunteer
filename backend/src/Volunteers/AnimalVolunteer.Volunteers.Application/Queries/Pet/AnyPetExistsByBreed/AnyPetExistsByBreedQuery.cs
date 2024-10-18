using AnimalVolunteer.Core.Abstractions.CQRS;

namespace AnimalVolunteer.Volunteers.Application.Queries.Pet.AnyPetExistsByBreed;

public record AnyPetExistsByBreedQuery(Guid BreedId) : IQuery;
