using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.VolunteerRequests.Application.Interfaces;
public interface IReadOnlyRepository
{
    public Task<bool> VolunteerRequestExistsByUserId(Guid userId, CancellationToken token = default);
    public Task<Result<DateTime?, Error>> GetLastRequestDate(Guid userId, CancellationToken token = default);
}
