using AnimalVolunteer.Domain.Aggregates.Volunteer;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Volunteer;

namespace AnimalVolunteer.Application.Interfaces;

public interface IVolunteerRepository
{
    public Task<Volunteer?> GetById(VolunteerId id, CancellationToken cancellationToken);
    public Task<bool> ExistByEmail(Email email, CancellationToken cancellationToken);
    public Task Create(Volunteer volunteer, CancellationToken cancellationToken);
    public Task Save(Volunteer volunteer, CancellationToken cancellationToken);
}
