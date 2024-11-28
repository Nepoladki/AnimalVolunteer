using AnimalVolunteer.Core.DTOs.Discussions;
using AnimalVolunteer.Discussions.Application.Interfaces;
using AnimalVolunteer.Discussions.Infrastructure.Linq2db;
using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Discussions.Infrastructure.Repositories;
public class ReadOnlyRepository : IReadOnlyRepository
{
    private readonly Linq2dbConnection _connection;
    public ReadOnlyRepository(Linq2dbConnection connection)
    {
        _connection = connection;
    }
    public Result<DiscussionDto, Error> GetDiscussionByRelatedId(Guid id)
    {
        var discussion = _connection.Discussions.FirstOrDefault(d => d.RelatedId == id);
        if (discussion is null)
            return Errors.General.NotFound(id);

        return discussion;
    }
}
