using AnimalVolunteer.Application.DTOs.Volunteer;
using AnimalVolunteer.Application.Interfaces;

namespace AnimalVolunteer.Volunteers.Application.Commands.Volunteer.Create;

public record CreateVolunteerCommand(
    FullNameDto FullName,
    string Email,
    string Description,
    IEnumerable<SocialNetworkDto> SocialNetworkList,
    IEnumerable<PaymentDetailsDto> PaymentDetailsList) : ICommand;
