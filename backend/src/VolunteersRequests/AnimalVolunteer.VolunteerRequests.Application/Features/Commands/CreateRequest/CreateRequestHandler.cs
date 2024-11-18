using AnimalVolunteer.Core;
using AnimalVolunteer.Core.Abstractions;
using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using AnimalVolunteer.VolunteerRequests.Application.Interfaces;
using AnimalVolunteer.VolunteerRequests.Domain;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Commands.CreateRequest;
public class CreateRequestHandler : ICommandHandler<CreateRequestCommand>
{
    private readonly IVolunteerRequestsRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateRequestCommand> _commandValidator;
    private readonly IReadOnlyRepository _readOnlyRepository;

    public CreateRequestHandler(
        IVolunteerRequestsRepository repository,
        [FromKeyedServices(Modules.VolunteerRequests)]IUnitOfWork unitOfWork,
        IValidator<CreateRequestCommand> commandValidator,
        IReadOnlyRepository readOnlyRepository)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _commandValidator = commandValidator;
        _readOnlyRepository = readOnlyRepository;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        CreateRequestCommand command, CancellationToken cancellationToken)
    {
        var validationResult = _commandValidator.Validate(command);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var requestAlreadyExist = await _readOnlyRepository
            .VolunteerRequestExistsByUserId(command.UserId, cancellationToken);
        if (requestAlreadyExist == true)
            return Errors.VolunteerRequests.AlreadyExist().ToErrorList();

        var requestId = VolunteerRequestId.Create();

        var userId = UserId.CreateWithGuid(command.UserId);

        var request = VolunteerRequest.Create(
            requestId,
            userId);

        _repository.Add(request);

        await _unitOfWork.SaveChanges(cancellationToken);

        return new UnitResult<ErrorList>();
    }
}
