using AnimalVolunteer.Core;
using AnimalVolunteer.Core.Abstractions;
using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.Discussions.Contracts;
using AnimalVolunteer.Discussions.Contracts.Requests;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using AnimalVolunteer.VolunteerRequests.Application.Interfaces;
using AnimalVolunteer.VolunteerRequests.Domain;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Commands.TakeRequestForConsideration;
public class TakeRequestForConsiderationHandler : ICommandHandler<TakeRequestForConsiderationCommand>
{
    private readonly IDiscussionsContract _discussonsContract;
    private readonly IValidator<TakeRequestForConsiderationCommand> _validator;
    private readonly IVolunteerRequestsRepository _volunteerRequestsRepository;
    private readonly ILogger<TakeRequestForConsiderationHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public TakeRequestForConsiderationHandler(
        IDiscussionsContract discussonsContract,
        ILogger<TakeRequestForConsiderationHandler> logger,
        IVolunteerRequestsRepository volunteerRequestsRepository,
        IValidator<TakeRequestForConsiderationCommand> validator,
        [FromKeyedServices(Modules.VolunteerRequests)] IUnitOfWork unitOfWork)
    {
        _discussonsContract = discussonsContract;
        _logger = logger;
        _volunteerRequestsRepository = volunteerRequestsRepository;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        TakeRequestForConsiderationCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var volunteerRequest = await _volunteerRequestsRepository.GetById(command.RequestId, cancellationToken);
        if (volunteerRequest.IsFailure)
            return volunteerRequest.Error.ToErrorList();

        var discussionRequest = new CreateDiscussionRequest(command.RequestId, command.UserId, command.AdminId);

        var discussionId = await _discussonsContract.CreateNewDiscussion(discussionRequest, cancellationToken);
        if (discussionId.IsFailure)
            return discussionId.Error;

        volunteerRequest.Value.TakeOnCosideration(command.AdminId, discussionId.Value);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation(
            "Volunteer request {vrId} was taken on consideration by admin {aId}",
            command.RequestId,
            command.AdminId);

        return UnitResult.Success<ErrorList>();
    }
}
