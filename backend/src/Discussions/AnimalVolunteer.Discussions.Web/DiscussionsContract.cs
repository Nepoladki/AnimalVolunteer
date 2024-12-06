using AnimalVolunteer.Discussions.Application.Features.Commands.CreateDiscussion;
using AnimalVolunteer.Discussions.Contracts;
using AnimalVolunteer.Discussions.Contracts.Requests;
using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Discussions.Web;

public class DiscussionsContract : IDiscussionsContract
{
    private readonly CreateDiscussionHandler _createDiscussionHandler;

    public DiscussionsContract(CreateDiscussionHandler createDiscussionHandler)
    {
        _createDiscussionHandler = createDiscussionHandler;
    }

    public async Task<Result<Guid, ErrorList>> CreateNewDiscussion(CreateDiscussionRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateDiscussionCommand(request.RelatedId, request.UserId, request.AdminId);

        return await _createDiscussionHandler.Handle(command, cancellationToken);
    }
}

