using AnimalVolunteer.Application.DTOs.Volunteer;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Pet.UpdatePet;
using AnimalVolunteer.Domain.Aggregates.VolunteerManagement.Enums;

namespace AnimalVolunteer.API.Controllers.Volunteer.Requests.Pet;

public record UpdatePetRequest(
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
    CurrentStatus CurrentStatus,
    IEnumerable<PaymentDetailsDto> PaymentDetails,
    IEnumerable<ContactInfoDto> ContactInfo)
{
    public UpdatePetCommand ToCommand(Guid volunteerId, Guid petId)
    {
        return new(
            volunteerId,
            petId,
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
            CurrentStatus,
            PaymentDetails,
            ContactInfo);
    }
}
