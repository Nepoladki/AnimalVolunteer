using AnimalVolunteer.Core.DTOs.Discussions;
using AnimalVolunteer.Discussions.Application.Interfaces;
using AnimalVolunteer.Discussions.Infrastructure.Linq2db;

namespace AnimalVolunteer.Discussions.Infrastructure.Repositories;
public class ReadOnlyRepository : IReadOnlyRepository
{
    private readonly Linq2dbConnection _connection;
    public ReadOnlyRepository(Linq2dbConnection connection)
    {
        _connection = connection;
    }
    public DiscussionDto GetDiscussionById(Guid id, CancellationToken cancellationToken)
    {
        return _connection.Discussions.FirstOrDefault(d => d.Id == id);
    }
}
