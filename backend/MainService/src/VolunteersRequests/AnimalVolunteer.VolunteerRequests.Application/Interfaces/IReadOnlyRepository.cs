using AnimalVolunteer.Core.DTOs.VolunteerRequests;
using AnimalVolunteer.Core.Models;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.VolunteerRequests.Domain.Enums;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.VolunteerRequests.Application.Interfaces;
public interface IReadOnlyRepository
{
    Task<PagedList<VolunteerRequestDto>> GetRequestsForConsideration(
        int Page, int PageSize, CancellationToken cancellationToken);
    Task<bool> VolunteerRequestExistsByUserId(Guid userId, CancellationToken token = default);
    Task<Result<DateTime?, Error>> GetLastRequestDate(Guid userId, CancellationToken token = default);

    Task<PagedList<VolunteerRequestDto>> GetRequestsByAdminId(
        Guid AdminId,
        int Page,
        int PageSize,
        CancellationToken cancellationToken,
        VolunteerRequestStatus? Status);

    Task<Result<VolunteerRequestDto, ErrorList>> GetRequestByUserId(
       Guid UserId, CancellationToken cancellationToken);
}
