using AnimalVolunteer.Core.DTOs.Common;

namespace AnimalVolunteer.Core.DTOs.Accounts;

public class UserDto
{
    public Guid Id { get; init; }
    public string FirstName { get; init; } = default!;
    public string? Patronymic {  get; init; }
    public string LastName { get; init; } = default!;
    public string? Photo {  get; init; }
    public List<SocialNetworkDto> SocialNetworks { get; init; } = [];
    public AdminAccountDto? AdminAccount { get; set; }
    public ParticipantAccountDto? ParticipantAccount { get; set; }
    public VolunteerAccountDto? VolunteerAccount { get; set; }
}

