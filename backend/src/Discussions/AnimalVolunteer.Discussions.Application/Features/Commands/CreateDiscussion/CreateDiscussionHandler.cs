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

namespace AnimalVolunteer.Discussions.Application.Features.Commands.CreateDiscussion;
public class CreateDiscussionHandler : ICommandHandler<CreateDiscussionCommand>
{
    private readonly IValidator<CreateDiscussionCommand> _validator;
    private readonly IDiscussionsRepository _discussionsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDiscussionHandler(
        IValidator<CreateDiscussionCommand> validator,
        IDiscussionsRepository discussionsRepository,
        [FromKeyedServices(Modules.Discussions)]IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _discussionsRepository = discussionsRepository;
        _unitOfWork = unitOfWork;
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

        return UnitResult.Success<ErrorList>();
    }
}
