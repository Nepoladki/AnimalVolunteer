using AnimalVolunteer.Domain.Aggregates.Volunteer.Root;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Volunteer;
using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Application.Interfaces;

public interface IVolunteerRepository
{
    public Task<Result<Volunteer, Error>> GetById(VolunteerId id, CancellationToken cancellationToken);
    public Task<bool> ExistByEmail(Email email, CancellationToken cancellationToken);
    public Task Create(Volunteer volunteer, CancellationToken cancellationToken);
    public Task Save(Volunteer volunteer, CancellationToken cancellationToken);
}
