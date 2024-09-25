using AnimalVolunteer.Application.Database;
using AnimalVolunteer.Domain.Aggregates.Volunteer;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Volunteer;
using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace AnimalVolunteer.Application.Interfaces;

public class VolunteerRepository : IVolunteerRepository
{
    private readonly IApplicationDbContext _dbContext;

    public VolunteerRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Volunteer, Error>> GetById
        (VolunteerId id, CancellationToken cancellationToken = default)
    {
        var volunteer = await _dbContext.Volunteers
            .Include(v => v.Pets).FirstOrDefaultAsync(v => v.Id == id, cancellationToken);

        if (volunteer is null)
            return Errors.General.NotFound(id);

        return volunteer;
    }

    public async Task<Volunteer?> GetByEmail(
        Email email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Volunteers.
            FirstOrDefaultAsync(v => v.Email == email, cancellationToken);
    }

    public async Task<bool> ExistByEmail(
        Email email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Volunteers
            .AnyAsync(v => v.Email == email, cancellationToken);
    }
    public async Task Create(
        Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        _dbContext.Volunteers.Add(volunteer);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Save(
        Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        _dbContext.Attach(volunteer);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
