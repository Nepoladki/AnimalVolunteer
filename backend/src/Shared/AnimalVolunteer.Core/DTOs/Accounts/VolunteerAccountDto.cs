using AnimalVolunteer.Core.DTOs.Common;

namespace AnimalVolunteer.Core.DTOs.Accounts;

public record VolunteerAccountDto(
    Guid Id,
    Guid UserId,
    int? Expirience,
    List<PaymentDetailsDto>? PaymentDetails,
    List<CertificateDto>? Certificates);

