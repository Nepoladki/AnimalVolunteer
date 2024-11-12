using AnimalVolunteer.Discussions.Application.Interfaces;
using AnimalVolunteer.Discussions.Domain.Aggregate;
using AnimalVolunteer.Discussions.Infrastructure.DbContexts;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace AnimalVolunteer.Discussions.Infrastructure.Repositories;

public class DiscussionsRepository : IDiscussionsRepository
{
    private readonly WriteDbContext _dbContext;

    public DiscussionsRepository(WriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Discussion, Error>> GetById(DiscussionId id, CancellationToken token)
    {
        var discussion = await _dbContext.Discussions.FirstOrDefaultAsync(c => c.Id == id, token);
        if (discussion is null)
            return Errors.General.NotFound(id);

        return discussion;
    }

    public void Add(Discussion discussion) 
    {
        _dbContext.Discussions.Add(discussion);
    }
}

