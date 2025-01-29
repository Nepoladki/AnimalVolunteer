using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using AnimalVolunteer.VolunteerRequests.Application.Interfaces;
using AnimalVolunteer.VolunteerRequests.Domain;
using AnimalVolunteer.VolunteerRequests.Infrastructure.DbContexts;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace AnimalVolunteer.VolunteerRequests.Infrastructure.Repositories;

public class VolunteerRequestsRepository : IVolunteerRequestsRepository
{
    private readonly WriteDbContext _context;

    public VolunteerRequestsRepository(WriteDbContext writeDbContext)
    {
        _context = writeDbContext;
    }

    public async Task<Result<VolunteerRequest, Error>> GetById(
        VolunteerRequestId id, CancellationToken token = default)
    {
        var volunteerRequest = await _context.VolunteerRequests
            .FirstOrDefaultAsync(x => x.Id == id, token);
        if (volunteerRequest is null)
            return Errors.General.NotFound(id);

        return volunteerRequest;
    }

    public void Add(VolunteerRequest volunteerRequest)
    {
        _context.VolunteerRequests.Add(volunteerRequest);
    }
}

