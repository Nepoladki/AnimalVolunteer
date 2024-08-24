using AnimalVolunteer.Application.DTOs;

namespace AnimalVolunteer.Application.Features.CreateVolunteer
{
    public record CreateVolunteerRequest(
        string FirstName,
        string SurName,
        string LastName,
        string Description,
        int ExpirienceYears,
        int PetsFoundedHome,
        int PetsLookingForHome,
        int PetsInVetClinic,
        List<ContactInfoDto> ContactInfoList,
        List<SocialNetworkDto> SocialNetworkList,
        List<PaymentDetailsDto> PaymentDetailsList);
}
