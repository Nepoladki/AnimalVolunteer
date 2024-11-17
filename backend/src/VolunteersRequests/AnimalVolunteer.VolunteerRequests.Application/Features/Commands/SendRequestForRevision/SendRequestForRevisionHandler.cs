using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Commands.SendRequestForRevision;
public class SendRequestForRevisionHandler : ICommandHandler<SendRequestForRevisionCommand>
{
    public Task<UnitResult<ErrorList>> Handle(
        SendRequestForRevisionCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
