﻿using AnimalVolunteer.Domain.Aggregates.Volunteer;
using AnimalVolunteer.Infrastructure;

namespace AnimalVolunteer.Application.Interfaces;

public class VolunteerRepository : IVolunteerRepository
{
    private readonly ApplicationDbContext _dbContext;

    public VolunteerRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(Volunteer volunteer, CancellationToken cancellationToken)
    {
        _dbContext.Volunteers.Add(volunteer);
        await _dbContext.SaveChangesAsync();
    }
}
