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

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Commands.ApproveRequest;
public class ApproveRequestHandler : ICommandHandler<ApproveRequestCommand>
{
    private readonly IValidator<ApproveRequestCommand> _validator;
    private readonly IVolunteerRequestsRepository _volunteerRequestsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ApproveRequestHandler> _logger;

    public ApproveRequestHandler(
        IValidator<ApproveRequestCommand> validator, 
        IVolunteerRequestsRepository volunteerRequestsRepository, 
        [FromKeyedServices(Modules.VolunteerRequests)]IUnitOfWork unitOfWork, 
        ILogger<ApproveRequestHandler> logger)
    {
        _validator = validator;
        _volunteerRequestsRepository = volunteerRequestsRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        ApproveRequestCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var volunteerRequest = await _volunteerRequestsRepository
            .GetById(command.RequestId, cancellationToken);
        if (volunteerRequest.IsFailure)
            return volunteerRequest.Error.ToErrorList();

        var approveResult = volunteerRequest.Value.ApproveRequest();
        if (approveResult.IsFailure)
            return approveResult.Error.ToErrorList();

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Volunteer request {vrId} was approved", command.RequestId);

        return UnitResult.Success<ErrorList>();
    }
}
