using AnimalVolunteer.Core.Abstractions;
using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using AnimalVolunteer.VolunteerRequests.Application.Interfaces;
using AnimalVolunteer.VolunteerRequests.Domain;
using CSharpFunctionalExtensions;
using FluentValidation;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Commands.CreateRequest;
public class CreateRequestHandler : ICommandHandler<CreateRequestCommand>
{
    private readonly IVolunteerRequestsRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateRequestCommand> _commandValidator;

    public CreateRequestHandler(
        IVolunteerRequestsRepository repository, 
        IUnitOfWork unitOfWork, 
        IValidator<CreateRequestCommand> commandValidator)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _commandValidator = commandValidator;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        CreateRequestCommand command, CancellationToken cancellationToken)
    {
        var validationResult = _commandValidator.Validate(command);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var requestId = VolunteerRequestId.Create();

        // var userId = ; Контракт в модуле Accounts?

        //var request = VolunteerRequest.Create(
        //    requestId,
        //    userId);

        return new UnitResult<ErrorList>();
    }
}
