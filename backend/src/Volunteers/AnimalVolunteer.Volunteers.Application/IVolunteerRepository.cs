using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using AnimalVolunteer.Volunteers.Domain.Root;
using AnimalVolunteer.Volunteers.Domain.ValueObjects.Volunteer;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Volunteers.Application;

public interface IVolunteerRepository
{
    public Task<Result<Volunteer, Error>> GetById(VolunteerId id, CancellationToken cancellationToken);
    public Task<bool> ExistByEmail(Email email, CancellationToken cancellationToken);
    public Task Create(Volunteer volunteer, CancellationToken cancellationToken);
    public Task Save(Volunteer volunteer, CancellationToken cancellationToken);
}
