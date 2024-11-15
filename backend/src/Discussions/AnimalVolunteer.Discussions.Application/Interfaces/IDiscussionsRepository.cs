using AnimalVolunteer.Discussions.Domain.Aggregate;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Discussions.Application.Interfaces;
public interface IDiscussionsRepository
{
    Task<Result<Discussion, Error>> GetById(DiscussionId id, CancellationToken token);
    void Add(Discussion discussion);
}
