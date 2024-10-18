using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;

namespace AnimalVolunteer.Volunteers.Contracts.Requests;

public record AnyPetExistsBySpeciesRequest(Guid SpeciesId);
