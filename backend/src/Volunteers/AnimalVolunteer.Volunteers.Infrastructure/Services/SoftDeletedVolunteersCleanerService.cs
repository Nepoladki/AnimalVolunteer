using AnimalVolunteer.Core;
using AnimalVolunteer.Core.BackgroundServices;
using AnimalVolunteer.Volunteers.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalVolunteer.Volunteers.Infrastructure.Services;
public class SoftDeletedVolunteersCleanerService : ISoftDeletedCleaner
{
    private readonly WriteDbContext _writeDbContext;
    private readonly ReadDbContext _readDbContext;

    public SoftDeletedVolunteersCleanerService(
        WriteDbContext writeDbContext,
        ReadDbContext readDbContext)
    {
        _writeDbContext = writeDbContext;
        _readDbContext = readDbContext;
    }

    public void Process(CancellationToken cancellationToken)
    {
        var volunteersToDelete = _readDbContext.Volunteers.Where(v => v.IsDeleted == true);
        var petsToDelete = _readDbContext.Pets.Where(p => p.IsDeleted == true);
    }
}
