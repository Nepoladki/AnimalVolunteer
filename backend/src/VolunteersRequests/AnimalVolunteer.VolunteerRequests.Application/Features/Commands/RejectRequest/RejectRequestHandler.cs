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

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Commands.RejectRequest;
public class RejectRequestHandler : ICommandHandler<RejectRequestCommand>
{
    private readonly IValidator<RejectRequestCommand> _validator;
    private readonly IVolunteerRequestsRepository _volunteerRequestsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RejectRequestHandler> _logger;

    public RejectRequestHandler(
        ILogger<RejectRequestHandler> logger, 
        [FromKeyedServices(Modules.VolunteerRequests)]IUnitOfWork unitOfWork, 
        IVolunteerRequestsRepository volunteerRequestsRepository, 
        IValidator<RejectRequestCommand> validator)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _volunteerRequestsRepository = volunteerRequestsRepository;
        _validator = validator;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        RejectRequestCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var volunteerRequest = await _volunteerRequestsRepository
            .GetById(command.RequestId, cancellationToken);
        if (volunteerRequest.IsFailure)
            return volunteerRequest.Error.ToErrorList();

        var rejectResult = volunteerRequest.Value.Reject(command.RejectionComment);
        if (rejectResult.IsFailure)
            return rejectResult.Error.ToErrorList();

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Volunteer request {vrId} was rejected", command.RequestId);

        return UnitResult.Success<ErrorList>();
    }
}
