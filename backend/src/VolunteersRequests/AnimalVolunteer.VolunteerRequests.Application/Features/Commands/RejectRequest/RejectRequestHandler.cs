using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Commands.RejectRequest;
public class RejectRequestHandler : ICommandHandler<RejectRequestCommand>
{
    public Task<UnitResult<ErrorList>> Handle(RejectRequestCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
