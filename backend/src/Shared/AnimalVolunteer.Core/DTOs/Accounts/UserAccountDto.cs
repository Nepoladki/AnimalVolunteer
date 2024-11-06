using AnimalVolunteer.Core.DTOs.Common;

namespace AnimalVolunteer.Core.DTOs.Accounts;

public record UserAccountDto(
    Guid UserId,
    Guid? ParticipantAccountId,
    Guid? VolunteerAccountId,
    Guid? AdminAccountId,
    FullNameDto FullName,
    string? Photo,
    int? Expirience,
    List<SocialNetworkDto>? SocialNetworks,
    List<PaymentDetailsDto>? PaymentDetails,
    List<CertificateDto>? Certificates);

