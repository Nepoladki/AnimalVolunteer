using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.VolunteerRequests.Application.Interfaces;
using AnimalVolunteer.VolunteerRequests.Infrastructure.Linq2db.Connections;
using CSharpFunctionalExtensions;
using LinqToDB;

namespace AnimalVolunteer.VolunteerRequests.Infrastructure.Repositories;

public class ReadOnlyRepository : IReadOnlyRepository
{
    private readonly Linq2DbConnection _connection;

    public ReadOnlyRepository(Linq2DbConnection connection)
    {
        _connection = connection;
    }

    public async Task<Result<DateTime?, Error>> GetLastRequestDate(Guid userId, CancellationToken token = default)
    {
        var request = await _connection.VolunteerRequests.FirstOrDefaultAsync(r => r.UserId == userId, token);
        if (request is null)
            return Errors.General.NotFound(userId);

        return request.LastRejectionAt;
    }

    public async Task<bool> VolunteerRequestExistsByUserId(Guid userId, CancellationToken token = default) =>
        await _connection.VolunteerRequests.AnyAsync(request => request.UserId == userId, token);
    
}

