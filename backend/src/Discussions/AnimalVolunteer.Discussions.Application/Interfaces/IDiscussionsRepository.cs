using AnimalVolunteer.Discussions.Domain.Aggregate;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Discussions.Application.Interfaces;
public interface IDiscussionsRepository
{
    Task<Result<Discussion, Error>> GetById(Guid Id, CancellationToken token);
    Task<Result<Discussion, Error>> GetByRelatedId(Guid relatedId, CancellationToken token);
    void Add(Discussion discussion);
}
