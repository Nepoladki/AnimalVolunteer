using AnimalVolunteer.Domain.Aggregates.Volunteer;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Volunteer;
using AnimalVolunteer.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AnimalVolunteer.Application.Interfaces;

public class VolunteerRepository : IVolunteerRepository
{
    private readonly ApplicationDbContext _dbContext;

    public VolunteerRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Volunteer?> GetByID(VolunteerId id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Volunteers
            .Include(v => v.Pets).FirstOrDefaultAsync(v => v.Id == id, cancellationToken);
    }

    public async Task Create(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        _dbContext.Volunteers.Add(volunteer);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Save(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        _dbContext.Attach(volunteer);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
