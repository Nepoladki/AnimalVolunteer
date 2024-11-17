using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Commands.ApproveRequest;
public class ApproveRequestHandler : ICommandHandler<ApproveRequestCommand>
{
    public Task<UnitResult<ErrorList>> Handle(ApproveRequestCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
