using AnimalVolunteer.Core;
using AnimalVolunteer.Core.Abstractions;
using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.Discussions.Application.Interfaces;
using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AnimalVolunteer.Discussions.Application.Features.Commands.CloseDiscussion;
public class CloseDiscussionHandler : ICommandHandler<CloseDiscussionCommand>
{
    private readonly IDiscussionsRepository _discussionsRepository;
    private readonly IValidator<CloseDiscussionCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CloseDiscussionHandler> _logger;

    public CloseDiscussionHandler(
        IDiscussionsRepository discussionsRepository,
        IValidator<CloseDiscussionCommand> validator,
        [FromKeyedServices(Modules.Discussions)] IUnitOfWork unitOfWork,
        ILogger<CloseDiscussionHandler> logger)
    {
        _discussionsRepository = discussionsRepository;
        _validator = validator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        CloseDiscussionCommand command, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(command);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var discussionResult = await _discussionsRepository
            .GetById(command.DiscussionId, cancellationToken);
        if (discussionResult.IsFailure)
            return discussionResult.Error.ToErrorList();

        var discussion = discussionResult.Value;

        discussion.Close();

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Discussion with id {dId} was closed", command.DiscussionId);

        return UnitResult.Success<ErrorList>();
    }
}
