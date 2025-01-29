using AnimalVolunteer.Core;
using AnimalVolunteer.Core.Abstractions;
using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using AnimalVolunteer.VolunteerRequests.Application.Interfaces;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Commands.UpdateRequest;
public class UpdateRequestHandler : ICommandHandler<UpdateRequestCommand>
{
    private readonly IVolunteerRequestsRepository _volunteerRequestsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateRequestCommand> _validator;
    private readonly ILogger<UpdateRequestHandler> _logger;

    public UpdateRequestHandler(
        ILogger<UpdateRequestHandler> logger,
        IValidator<UpdateRequestCommand> validator,
        [FromKeyedServices(Modules.VolunteerRequests)] IUnitOfWork unitOfWork,
        IVolunteerRequestsRepository volunteerRequestsRepository)
    {
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
        _volunteerRequestsRepository = volunteerRequestsRepository;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        UpdateRequestCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var requestId = VolunteerRequestId.CreateWithGuid(command.RequestId);

        var volunteerRequest = await _volunteerRequestsRepository
            .GetById(requestId, cancellationToken);
        if (volunteerRequest.IsFailure)
            return volunteerRequest.Error.ToErrorList();

        var newVolunteerInfo = VolunteerInfo.Create(
            command.VolunteerInfo.ExpirienceDescription, 
            command.VolunteerInfo.Passport).Value;

        var updateResult = volunteerRequest.Value.UpdateRequest(newVolunteerInfo);
        if (updateResult.IsFailure)
            return updateResult.Error.ToErrorList();

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation(
            "Volunteer request {vrId} was updated", 
            volunteerRequest.Value.Id);

        return UnitResult.Success<ErrorList>();
    }
}
