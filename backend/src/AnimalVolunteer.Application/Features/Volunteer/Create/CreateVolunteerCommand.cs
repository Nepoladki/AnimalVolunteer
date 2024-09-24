using AnimalVolunteer.Application.DTOs.Volunteer;

namespace AnimalVolunteer.Application.Features.Volunteer.CreateVolunteer;

public record CreateVolunteerCommand(
    FullNameDto FullName,
    string Email,
    string Description,
    List<SocialNetworkDto> SocialNetworkList,
    List<PaymentDetailsDto> PaymentDetailsList);
