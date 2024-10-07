using AnimalVolunteer.Application.DTOs.Volunteer;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Pet.AddPet;
using AnimalVolunteer.Domain.Aggregates.Volunteer.Enums;

namespace AnimalVolunteer.API.Controllers.Volunteer.Requests.Pet;

public record AddPetRequest(
    string Name,
    string Description,
    string Color,
    double Weight,
    double Height,
    Guid SpeciesId,
    Guid BreedId,
    string HealthDescription,
    bool IsVaccinated,
    bool IsNeutered,
    string Country,
    string City,
    string Street,
    string? House,
    DateOnly BirthDate,
    CurrentStatus CurrentStatus)
{
    public AddPetCommand ToCommand(Guid volunteerId)
    {
        return new(
            volunteerId,
            Name,
            Description,
            Color,
            Weight,
            Height,
            SpeciesId,
            BreedId,
            HealthDescription,
            IsVaccinated,
            IsNeutered,
            Country,
            City,
            Street,
            House,
            BirthDate,
            CurrentStatus);
    }
}
