using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.DTOs.Common;

namespace AnimalVolunteer.Volunteers.Application.Commands.Volunteer.Create;

public record CreateVolunteerCommand(
    FullNameDto FullName,
    string Email,
    string Description,
    IEnumerable<SocialNetworkDto> SocialNetworkList,
    IEnumerable<PaymentDetailsDto> PaymentDetailsList) : ICommand;
