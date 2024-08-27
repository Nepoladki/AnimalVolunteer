using AnimalVolunteer.Application.DTOs;

namespace AnimalVolunteer.Application.Features.CreateVolunteer;

public record CreateVolunteerRequest(
    FullNameDto FullName,
    string Description,
    List<SocialNetworkDto> SocialNetworkList,
    List<PaymentDetailsDto> PaymentDetailsList);

public record FullNameDto(
string FirstName, 
string SurName,
string LastName);