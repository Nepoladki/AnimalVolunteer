﻿using AnimalVolunteer.Core;
using AnimalVolunteer.Core.Abstractions;
using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.DTOs.Discussions;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.Discussions.Application.Features.Queries.GetDiscussionById;
using AnimalVolunteer.Discussions.Application.Interfaces;
using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AnimalVolunteer.Discussions.Application.Features.Queries.GetDiscussionByRelatedId;
public class GetDiscussionByRelatedIdHandler : ICommandHandler<DiscussionDto, GetDiscussionByRelatedIdCommand>
{
    private readonly IValidator<GetDiscussionByRelatedIdCommand> _validator;
    private readonly ILogger<GetDiscussionByRelatedIdHandler> _logger;
    private readonly IReadOnlyRepository _readOnlyRepository;
    public GetDiscussionByRelatedIdHandler(
        IValidator<GetDiscussionByRelatedIdCommand> validator,
        ILogger<GetDiscussionByRelatedIdHandler> logger,
        IReadOnlyRepository readOnlyRepository)
    {
        _validator = validator;
        _logger = logger;
        _readOnlyRepository = readOnlyRepository;
    }

    public async Task<Result<DiscussionDto, ErrorList>> Handle(
        GetDiscussionByRelatedIdCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var discussion = _readOnlyRepository.GetDiscussionByRelatedId(command.RelatedId);
        if (discussion.IsFailure)
        {
            _logger.LogError("Discussion eith RelatedId {rId} was not found", command.RelatedId);
            return discussion.Error.ToErrorList();
        }

        return discussion.Value;
    }
}
