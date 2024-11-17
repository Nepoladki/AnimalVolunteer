using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Commands.UpdateRequestAfterRevision;
public class UpdateRequestAfterRevisionHandler : ICommandHandler<UpdateRequestAfterRevisionCommand>
{
    public Task<UnitResult<ErrorList>> Handle(
        UpdateRequestAfterRevisionCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
