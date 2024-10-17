using AnimalVolunteer.Volunteers.Contracts.Requests;

namespace AnimalVolunteer.Volunteers.Contracts;

public interface IVolunteersContract
{
    Task<bool> AnyPetExistsByBreed(
        AnyPetExistsByBreedRequest request, CancellationToken cancellationToken = default);
    Task<bool> AnyPetExistsBySpecies(
        AnyPetExistsBySpeciesRequest request, CancellationToken cancellationToken = default);
}
