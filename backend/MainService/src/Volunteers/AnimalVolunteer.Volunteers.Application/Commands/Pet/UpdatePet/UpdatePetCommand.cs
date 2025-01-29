using AnimalVolunteer.Core.DTOs.Common;
using AnimalVolunteer.Core.DTOs.Volunteers;
using AnimalVolunteer.Volunteers.Domain.Enums;

namespace AnimalVolunteer.Volunteers.Application.Commands.Pet.UpdatePet;

public record UpdatePetCommand(
    Guid VolunteerId,
    Guid PetId,
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
    IEnumerable<ContactInfoDto> ContactInfo) : Core.Abstractions.CQRS.ICommand;
