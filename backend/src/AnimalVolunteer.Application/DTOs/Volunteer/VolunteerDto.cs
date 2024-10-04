using AnimalVolunteer.Application.DTOs.Volunteer.Pet;

namespace AnimalVolunteer.Application.DTOs.Volunteer;

public class VolunteerDto
{
    public Guid Id { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string Surname { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public int ExpirienceYears { get; init; }
    public int PetsFoundedHome { get; init; }
    public int PetsLookingForHome { get; init; }
    public int PetsInVetClinic { get; init; }
    public IEnumerable<ContactInfoDto> ContactInfos { get; init; } = default!;
    public IEnumerable<SocialNetworkDto> SocialNetworks { get; init; } = default!;
    public IEnumerable<PaymentDetailsDto> PaymentDetails { get; init; } = default!;
    public IEnumerable<PetDto> Pets { get; init; } = default!;
}
