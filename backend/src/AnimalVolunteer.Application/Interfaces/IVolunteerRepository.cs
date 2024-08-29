using AnimalVolunteer.Domain.Aggregates.Volunteer;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Volunteer;
using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Application.Interfaces;

public interface IVolunteerRepository
{
    public Task<Volunteer?> GetByID(VolunteerId id, CancellationToken cancellationToken);
    public Task Create(Volunteer volunteer, CancellationToken cancellationToken);
    public Task Save(Volunteer volunteer, CancellationToken cancellationToken);
}
