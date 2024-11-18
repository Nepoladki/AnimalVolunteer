using AnimalVolunteer.Core;
using AnimalVolunteer.Core.Abstractions;
using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.Discussions.Application.Interfaces;
using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalVolunteer.Discussions.Application.Features.Commands.CloseDiscussion;
public class CloseDiscussionHandler : ICommandHandler<CloseDiscussionCommand>
{
    private readonly IDiscussionsRepository _discussionsRepository;
    private readonly IValidator<CloseDiscussionCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public CloseDiscussionHandler(
        IDiscussionsRepository discussionsRepository,
        IValidator<CloseDiscussionCommand> validator,
        [FromKeyedServices(Modules.Discussions)]IUnitOfWork unitOfWork)
    {
        _discussionsRepository = discussionsRepository;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        CloseDiscussionCommand command, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(command);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var discussionResult = await _discussionsRepository
            .GetByRelatedId(command.RelatedId, cancellationToken);
        if (discussionResult.IsFailure)
            return discussionResult.Error.ToErrorList();

        var discussion = discussionResult.Value;

        discussion.Close();

        await _unitOfWork.SaveChanges(cancellationToken);

        return UnitResult.Success<ErrorList>();
    }
}
