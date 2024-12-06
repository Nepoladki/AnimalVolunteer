using AnimalVolunteer.Core;
using AnimalVolunteer.Core.Abstractions;
using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.VolunteerRequests.Application.Interfaces;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Commands.TakeRequestForReConsideration;
public class TakeRequestForReConsiderationHandler : ICommandHandler<TakeRequestForReConsiderationCommand>
{
    private readonly IValidator<TakeRequestForReConsiderationCommand> _validator;
    private readonly IVolunteerRequestsRepository _volunteerRequestsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<TakeRequestForReConsiderationHandler> _logger;

    public TakeRequestForReConsiderationHandler(
        IValidator<TakeRequestForReConsiderationCommand> validator,
        [FromKeyedServices(Modules.VolunteerRequests)] IUnitOfWork unitOfWork,
        IVolunteerRequestsRepository volunteerRequestsRepository,
        ILogger<TakeRequestForReConsiderationHandler> logger)
    {
        _validator = validator;
        _unitOfWork = unitOfWork;
        _volunteerRequestsRepository = volunteerRequestsRepository;
        _logger = logger;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        TakeRequestForReConsiderationCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var volunteerRequest = await _volunteerRequestsRepository
            .GetById(command.RequestId, cancellationToken);
        if (volunteerRequest.IsFailure)
            return volunteerRequest.Error.ToErrorList();

        var takeResult = volunteerRequest.Value.TakeOnReConsideration();
        if (takeResult.IsFailure)
            return takeResult.Error.ToErrorList();

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation(
            "Volunteer request {vrId} was sended to re-consideration", 
            volunteerRequest.Value.Id);

        return UnitResult.Success<ErrorList>();
    }
}
