using AnimalVolunteer.Core.DTOs.Discussions;

namespace AnimalVolunteer.Discussions.Application.Interfaces;
public interface IReadOnlyRepository
{
    DiscussionDto? GetDiscussionById(Guid id, CancellationToken cancellationToken);
}
