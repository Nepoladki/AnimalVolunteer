using AnimalVolunteer.Application.DTOs.Volunteer;

namespace AnimalVolunteer.Application.Features.Volunteer.Update.MainInfo;

public record UpdateVounteerMainInfoRequest(
    Guid Id,
    MainInfoDto Dto);
