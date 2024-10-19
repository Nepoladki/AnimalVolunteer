using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using FluentValidation;
using AnimalVolunteer.Core;
using Microsoft.Extensions.DependencyInjection;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.Core.Abstractions.CQRS;
using DomainEntities = AnimalVolunteer.SharedKernel.ValueObjects;

namespace AnimalVolunteer.Volunteers.Application.Commands.Volunteer.Update.PaymentDetails;

public class UpdateVolunteerPaymentDetailsHandler
    : ICommandHandler<Guid, UpdateVolunteerPaymentDetailsCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IValidator<UpdateVolunteerPaymentDetailsCommand> _validator;
    private readonly ILogger<UpdateVolunteerPaymentDetailsHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateVolunteerPaymentDetailsHandler
        (IVolunteerRepository volunteerRepository,
        ILogger<UpdateVolunteerPaymentDetailsHandler> logger,
        [FromKeyedServices(Modules.Volunteers)] IUnitOfWork unitOfWork,
        IValidator<UpdateVolunteerPaymentDetailsCommand> validator)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        UpdateVolunteerPaymentDetailsCommand command,
        CancellationToken cancellationToken)
    {
        var volunteerResult = await _volunteerRepository
            .GetById(command.Id, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var paymentDetails = command.PaymentDetails.Select(x =>
                DomainEntities.PaymentDetails.Create(x.Name, x.Description).Value).ToList();

        volunteerResult.Value.UpdatePaymentDetails(paymentDetails);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Volunteer {ID} updated", volunteerResult.Value.Id);

        return (Guid)volunteerResult.Value.Id;
    }
}
