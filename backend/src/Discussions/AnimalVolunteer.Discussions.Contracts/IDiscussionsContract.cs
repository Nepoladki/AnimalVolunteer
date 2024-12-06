using AnimalVolunteer.Discussions.Application.Features.Commands.CreateDiscussion;
using AnimalVolunteer.Discussions.Contracts.Requests;
using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Discussions.Contracts;
public interface IDiscussionsContract
{
    Task<Result<Guid, ErrorList>> CreateNewDiscussion(CreateDiscussionRequest request, CancellationToken cancellationToken = default); 
}
