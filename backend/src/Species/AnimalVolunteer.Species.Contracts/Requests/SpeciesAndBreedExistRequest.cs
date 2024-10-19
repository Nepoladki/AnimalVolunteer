using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;

namespace AnimalVolunteer.Species.Contracts.Requests;

public record SpeciesAndBreedExistRequest(SpeciesId SpeciesId, Guid BreedId);