using AnimalVolunteer.Volunteers.Application.Queries.Pet.AnyPetExistsByBreed;
using AnimalVolunteer.Volunteers.Application.Queries.Pet.AnyPetExistsBySpecies;
using AnimalVolunteer.Volunteers.Contracts;
using AnimalVolunteer.Volunteers.Contracts.Requests;

namespace AnimalVolunteer.Volunteers.Web;

public class VolunteersContract(
    AnyPetExistsByBreedHandler anyPetExistsByBreedHandler,
    AnyPetExistsBySpeciesHandler anyPetExistsBySpeciesHandler) : IVolunteersContract
{
    private readonly AnyPetExistsByBreedHandler _anyPetExistsByBreedHandler = anyPetExistsByBreedHandler;
    private readonly AnyPetExistsBySpeciesHandler _anyPetExistsBySpeciesHandler = anyPetExistsBySpeciesHandler;

    public async Task<bool> AnyPetExistsByBreed(
        AnyPetExistsByBreedRequest request, CancellationToken cancellationToken = default)
    {
        var query = new AnyPetExistsByBreedQuery(request.BreedId);

        return await _anyPetExistsByBreedHandler.Handle(query, cancellationToken);
    }

    public async Task<bool> AnyPetExistsBySpecies(
        AnyPetExistsBySpeciesRequest request, CancellationToken cancellationToken = default)
    {
        var query = new AnyPetExistsBySpeciesQuery(request.SpeciesId);

        return await _anyPetExistsBySpeciesHandler.Handle(query, cancellationToken);
    }
}
