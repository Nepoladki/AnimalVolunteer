using AnimalVolunteer.Core;
using AnimalVolunteer.Core.Abstractions;
using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.Discussions.Application.Interfaces;
using AnimalVolunteer.Discussions.Domain.Aggregate;
using AnimalVolunteer.Discussions.Domain.Aggregate.ValueObjects;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AnimalVolunteer.Discussions.Application.Features.Commands.AmendMessage;

public class AmendMessageHandler : ICommandHandler<AmendMessageCommand>
{
    private readonly IValidator<AmendMessageCommand> _validator;
    private readonly IDiscussionsRepository _discussionsRepository;
    private readonly ILogger<AmendMessageHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public AmendMessageHandler(
        IValidator<AmendMessageCommand> validator, 
        IDiscussionsRepository discussionsRepository, 
        ILogger<AmendMessageHandler> logger, 
        [FromKeyedServices(Modules.Discussions)]IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _discussionsRepository = discussionsRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        AmendMessageCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var discussionResult = await _discussionsRepository
            .GetById(DiscussionId.CreateWithGuid(command.DiscussionId), cancellationToken);
        if (discussionResult.IsFailure)
            return discussionResult.Error.ToErrorList();

        Discussion discussion = discussionResult.Value;

        Text newText = Text.Create(command.NewText).Value;

        var amendResult = discussion.AmendMessage(
            MessageId.CreateWithGuid(command.MessageId), 
            command.UserId, 
            newText);
        if (amendResult.IsFailure)
            return amendResult.Error.ToErrorList();

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation(
            "User with id {uid} amended message with id {mId}",
            command.UserId, 
            command.MessageId);

        return new UnitResult<ErrorList>();
    }
}

