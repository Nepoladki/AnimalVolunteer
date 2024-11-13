using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using AnimalVolunteer.VolunteerRequests.Domain;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.VolunteerRequests.Application.Interfaces;
public interface IVolunteerRequestsRepository
{
    Task<Result<VolunteerRequest, Error>> GetById(
        VolunteerRequestId id, CancellationToken token = default);
    void Add(VolunteerRequest volunteerRequest);
}
