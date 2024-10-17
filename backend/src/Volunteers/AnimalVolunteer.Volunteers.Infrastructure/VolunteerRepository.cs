using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using AnimalVolunteer.Volunteers.Application;
using AnimalVolunteer.Volunteers.Domain.Root;
using AnimalVolunteer.Volunteers.Domain.ValueObjects.Volunteer;
using AnimalVolunteer.Volunteers.Infrastructure.DbContexts;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace AnimalVolunteer.Volunteers.Infrastructure;

public class VolunteerRepository : IVolunteerRepository
{
    private readonly WriteDbContext _dbContext;

    public VolunteerRepository(WriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Volunteer, Error>> GetById
        (VolunteerId id, CancellationToken cancellationToken = default)
    {
        var volunteer = await _dbContext.Volunteers
            .Include(v => v.Pets)
            .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);

        if (volunteer is null)
            return Errors.General.NotFound(id);

        return volunteer!;
    }

    public async Task<Volunteer?> GetByEmail(
        Email email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Volunteers
            .Include(v => v.Pets)
            .FirstOrDefaultAsync(v => v.Email == email, cancellationToken);
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
        await _dbContext.Volunteers.AddAsync(volunteer, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Save(
        Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        _dbContext.Attach(volunteer);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
