using AnimalVolunteer.Core.DTOs.Accounts;

namespace AnimalVolunteer.Accounts.Application.Interfaces;
public interface IReadDbContext
{
    IQueryable<UserDto> Users { get; }
    IQueryable<ParticipantAccountDto> Participants { get; }
    IQueryable<VolunteerAccountDto> Volunteers { get; }
    IQueryable<AdminAccountDto> Admins { get; }
}
