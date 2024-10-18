using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;

namespace AnimalVolunteer.Species.Application.Queries.SpeciesAndBreedExists;

public record SpeciesAndBreedExistQuery(SpeciesId SpeciesId, Guid BreedId) : IQuery;
