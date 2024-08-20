using AnimalVolunteer.Domain.Aggregates;
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

    public async Task CreateAsync(Volunteer volunteer)
    {
        _dbContext.Volunteers.Add(volunteer);
        await _dbContext.SaveChangesAsync();
    }
}
