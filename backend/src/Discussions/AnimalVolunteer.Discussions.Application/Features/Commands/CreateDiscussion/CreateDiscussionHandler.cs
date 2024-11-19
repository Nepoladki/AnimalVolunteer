using AnimalVolunteer.Core;
using AnimalVolunteer.Core.Abstractions;
using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.Discussions.Application.Interfaces;
using AnimalVolunteer.Discussions.Domain.Aggregate;
using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AnimalVolunteer.Discussions.Application.Features.Commands.CreateDiscussion;
public class CreateDiscussionHandler : ICommandHandler<CreateDiscussionCommand>
{
    private readonly IValidator<CreateDiscussionCommand> _validator;
    private readonly IDiscussionsRepository _discussionsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateDiscussionHandler> _logger;

    public CreateDiscussionHandler(
        IValidator<CreateDiscussionCommand> validator,
        IDiscussionsRepository discussionsRepository,
        [FromKeyedServices(Modules.Discussions)]IUnitOfWork unitOfWork,
        ILogger<CreateDiscussionHandler> logger)
    {
        _validator = validator;
        _discussionsRepository = discussionsRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        CreateDiscussionCommand command, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(command);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var discussion = Discussion.Create(command.RelatedId, [command.UserId, command.AdminId]);
        if (discussion.IsFailure)
            return discussion.Error.ToErrorList();

        _discussionsRepository.Add(discussion.Value);

        await _unitOfWork.SaveChanges();

        _logger.LogInformation(
            "Discussion with Id {dId} created for reladed id {rId}",
            discussion.Value.Id,
            command.RelatedId);

        return UnitResult.Success<ErrorList>();
    }
}
