using AnimalVolunteer.Core;
using AnimalVolunteer.Core.Abstractions;
using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using AnimalVolunteer.VolunteerRequests.Application.Interfaces;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Commands.SendRequestForRevision;
public class SendRequestForRevisionHandler : ICommandHandler<SendRequestForRevisionCommand>
{
    private readonly IValidator<SendRequestForRevisionCommand> _validator;
    private readonly IVolunteerRequestsRepository _volunteerRequestsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SendRequestForRevisionHandler> _logger;

    public SendRequestForRevisionHandler(
        IValidator<SendRequestForRevisionCommand> validator, 
        IVolunteerRequestsRepository volunteerRequestsRepository, 
        [FromKeyedServices(Modules.VolunteerRequests)]IUnitOfWork unitOfWork, 
        ILogger<SendRequestForRevisionHandler> logger)
    {
        _validator = validator;
        _volunteerRequestsRepository = volunteerRequestsRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        SendRequestForRevisionCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var volunteerRequestId = VolunteerRequestId.CreateWithGuid(command.RequestId);

        var volunteerRequest = await _volunteerRequestsRepository
            .GetById(volunteerRequestId, cancellationToken);
        if (volunteerRequest.IsFailure)
            return volunteerRequest.Error.ToErrorList();

        var rejectResult = volunteerRequest.Value.SendOnRevision(command.RejectionComment);
        if (rejectResult.IsFailure)
            return rejectResult.Error.ToErrorList();

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("VolunteerRequest {vrId} sended for revision", command.RequestId);

        return UnitResult.Success<ErrorList>();
    }
}
