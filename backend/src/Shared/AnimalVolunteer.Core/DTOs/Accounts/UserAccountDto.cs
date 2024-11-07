using AnimalVolunteer.Core.DTOs.Common;

namespace AnimalVolunteer.Core.DTOs.Accounts;

public record UserAccountDto(
    Guid UserId,
    Guid? ParticipantAccountId,
    Guid? VolunteerAccountId,
    Guid? AdminAccountId,
    string FirstName,
    string? Patronymic,
    string LastName,
    string? Photo,
    int? Expirience,
    List<SocialNetworkDto>? SocialNetworks,
    List<PaymentDetailsDto>? PaymentDetails,
    List<CertificateDto>? Certificates);

