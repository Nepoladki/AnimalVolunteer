using AnimalVolunteer.Domain.Aggregates;

namespace AnimalVolunteer.Application.Interfaces;

public interface IVolunteerRepository
{
    public Task CreateAsync(Volunteer volunteer, CancellationToken cancellationToken);
}
