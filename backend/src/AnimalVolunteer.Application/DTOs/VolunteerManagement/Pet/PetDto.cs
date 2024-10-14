using AnimalVolunteer.Application.DTOs.Volunteer;
using AnimalVolunteer.Application.DTOs.Volunteer.Pet;
using AnimalVolunteer.Domain.Aggregates.VolunteerManagement.Enums;

namespace AnimalVolunteer.Application.DTOs.VolunteerManagement.Pet;

public class PetDto
{
    public Guid Id { get; init; }
    public Guid VolunteerId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Color { get; init; } = string.Empty;
    public double Weight { get; init; }
    public double Height { get; init; }
    public Guid SpeciesId { get; init; }
    public Guid BreedId { get; init; }
    public string HealthDescription { get; init; } = string.Empty;
    public bool IsVaccinated { get; init; }
    public bool IsNeutered { get; init; }
    public string Country { get; init; } = string.Empty;
    public string City { get; init; } = string.Empty;
    public string Street { get; init; } = string.Empty;
    public string House { get; init; } = string.Empty;
    public DateOnly BirthDate { get; init; }
    public CurrentStatus CurrentStatus { get; init; }
    public bool IsDeleted { get; init; }
    public IEnumerable<PaymentDetailsDto> PaymentDetails {  get; init; } = default!;
    public IEnumerable<PetPhotoDto> Photos { get; init; } = default!;
}
