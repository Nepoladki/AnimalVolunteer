using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;

namespace AnimalVolunteer.Volunteers.Application.Queries.Pet.AnyPetExistsBySpecies;

public record AnyPetExistsBySpeciesQuery(SpeciesId SpeciesId) : IQuery;
