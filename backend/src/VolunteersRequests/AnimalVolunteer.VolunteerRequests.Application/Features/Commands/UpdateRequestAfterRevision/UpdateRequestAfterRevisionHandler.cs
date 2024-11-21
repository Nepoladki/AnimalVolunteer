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

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Commands.UpdateRequestAfterRevision;
public class UpdateRequestAfterRevisionHandler : ICommandHandler<UpdateRequestAfterRevisionCommand>
{
    private readonly IVolunteerRequestsRepository _volunteerRequestsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateRequestAfterRevisionCommand> _validator;
    private readonly ILogger<UpdateRequestAfterRevisionHandler> _logger;

    public UpdateRequestAfterRevisionHandler(
        ILogger<UpdateRequestAfterRevisionHandler> logger, 
        IValidator<UpdateRequestAfterRevisionCommand> validator, 
        [FromKeyedServices(Modules.VolunteerRequests)]IUnitOfWork unitOfWork, 
        IVolunteerRequestsRepository volunteerRequestsRepository)
    {
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
        _volunteerRequestsRepository = volunteerRequestsRepository;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        UpdateRequestAfterRevisionCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var requestId = VolunteerRequestId.CreateWithGuid(command.RequestId);

        var volunteerRequest = await _volunteerRequestsRepository.GetById(requestId, cancellationToken);
        if (volunteerRequest.IsFailure)
            return volunteerRequest.Error.ToErrorList();

        volunteerRequest.Value.TakeOnConsiderationAfterRevision();
    }
}
