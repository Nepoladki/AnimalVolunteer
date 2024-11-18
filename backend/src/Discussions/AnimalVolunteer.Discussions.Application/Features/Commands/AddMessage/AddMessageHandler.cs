using AnimalVolunteer.Core;
using AnimalVolunteer.Core.Abstractions;
using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.Discussions.Application.Interfaces;
using AnimalVolunteer.Discussions.Domain.Aggregate.Entities;
using AnimalVolunteer.Discussions.Domain.Aggregate.ValueObjects;
using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalVolunteer.Discussions.Application.Features.Commands.AddMessage;
public class AddMessageHandler : ICommandHandler<AddMessageCommand>
{
    private readonly IDiscussionsRepository _discussionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddMessageCommand> _validator;

    public AddMessageHandler(
        IDiscussionsRepository discussionRepository,
        [FromKeyedServices(Modules.Discussions)] IUnitOfWork unitOfWork,
        IValidator<AddMessageCommand> validator)
    {
        _discussionRepository = discussionRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        AddMessageCommand command, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(command);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var discussionResult = await _discussionRepository
            .GetByRelatedId(command.RelatedId, cancellationToken);
        if (discussionResult.IsFailure)
            return discussionResult.Error.ToErrorList();

        var discussion = discussionResult.Value;

        Text text = Text.Create(command.Text).Value;

        Message message = Message.Create(command.UserId, text);
    }
}
