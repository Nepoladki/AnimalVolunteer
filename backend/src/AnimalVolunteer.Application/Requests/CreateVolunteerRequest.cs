using AnimalVolunteer.Application.DTOs;

namespace AnimalVolunteer.Application.Requests
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
