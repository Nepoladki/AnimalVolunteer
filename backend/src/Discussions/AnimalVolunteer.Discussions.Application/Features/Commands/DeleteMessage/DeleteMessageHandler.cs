using AnimalVolunteer.Core;
using AnimalVolunteer.Core.Abstractions;
using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.Discussions.Application.Interfaces;
using AnimalVolunteer.Discussions.Domain.Aggregate;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AnimalVolunteer.Discussions.Application.Features.Commands.DeleteMessage;

public class DeleteMessageHandler : ICommandHandler<DeleteMessageCommand>
{
    private readonly IValidator<DeleteMessageCommand> _validator;
    private readonly IDiscussionsRepository _discussionsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteMessageHandler> _logger;

    public DeleteMessageHandler(
        IValidator<DeleteMessageCommand> validator, 
        IDiscussionsRepository discussionsRepository, 
        [FromKeyedServices(Modules.Discussions)]IUnitOfWork unitOfWork, 
        ILogger<DeleteMessageHandler> logger)
    {
        _validator = validator;
        _discussionsRepository = discussionsRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        DeleteMessageCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var discussionResult = await _discussionsRepository
            .GetById(command.DiscussionId, cancellationToken);
        if (discussionResult.IsFailure)
            return discussionResult.Error.ToErrorList();

        Discussion discussion = discussionResult.Value;

        var messageResult = discussion.GetMessage(MessageId.CreateWithGuid(command.MessageId));
        if (messageResult.IsFailure)
            return messageResult.Error.ToErrorList();

        var deletingResult = discussion.DeleteMessage(messageResult.Value, command.UserId);
        if (deletingResult.IsFailure)
            return deletingResult.Error.ToErrorList();

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation(
            "User with id {uId} deleted Message with id {mId}", 
            command.UserId, 
            command.MessageId);

        return new UnitResult<ErrorList>();
    }
}

