using DomainPaymentDetails = AnimalVolunteer.Domain.Common.ValueObjects.PaymentDetails;
using AnimalVolunteer.Application.Interfaces;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using AnimalVolunteer.Application.Database;
using FluentValidation;
using AnimalVolunteer.Application.Extensions;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Common.ValueObjects;

namespace AnimalVolunteer.Application.Features.Volunteer.Update.PaymentDetails;

public class UpdateVolunteerPaymentDetailsHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IValidator<UpdateVolunteerPaymentDetailsCommand> _validator;
    private readonly ILogger<UpdateVolunteerPaymentDetailsHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateVolunteerPaymentDetailsHandler
        (IVolunteerRepository volunteerRepository,
        ILogger<UpdateVolunteerPaymentDetailsHandler> logger,
        IUnitOfWork unitOfWork,
        IValidator<UpdateVolunteerPaymentDetailsCommand> validator)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Update(
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

        var paymentDetails = PaymentDetailsList.Create(
            command.PaymentDetailsList.Value.Select(x => 
                DomainPaymentDetails.Create(x.Name, x.Description).Value));

        volunteerResult.Value.UpdatePaymentDetails(paymentDetails);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Volunteer {ID} updated", volunteerResult.Value.Id);

        return (Guid)volunteerResult.Value.Id;
    }
}
