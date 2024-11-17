using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Commands.TakeRequestOnConsideration;
public class TakeRequestForConsiderationHandler : ICommandHandler<TakeRequestForConsiderationCommand>
{
    public async Task<UnitResult<ErrorList>> Handle(
        TakeRequestForConsiderationCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
