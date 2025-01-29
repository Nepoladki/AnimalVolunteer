using AnimalVolunteer.Core.DTOs.Common;
using AnimalVolunteer.Core.DTOs.Volunteers;
using AnimalVolunteer.Volunteers.Application.Commands.Pet.UpdatePet;
using AnimalVolunteer.Volunteers.Domain.Enums;

namespace AnimalVolunteer.Volunteers.Web.Requests.Pet;

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
