using AnimalVolunteer.Accounts.Application.Interfaces;
using AnimalVolunteer.Accounts.Domain.Models;
using AnimalVolunteer.Core;
using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalVolunteer.Accounts.Application.Commands.UpdatePaymentDetails;

public class UpdatePaymentDetailsHandler : ICommandHandler<UpdatePaymentDetailsCommand>
{
    private readonly IAccountManager _accountManager;
    private readonly IValidator<UpdatePaymentDetailsCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePaymentDetailsHandler(
        IAccountManager accountManager, 
        IValidator<UpdatePaymentDetailsCommand> validator,
        [FromKeyedServices(Modules.Accounts)] IUnitOfWork unitOfWork)
    {
        _accountManager = accountManager;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        UpdatePaymentDetailsCommand command, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(command);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();

        var volunteerAccount = await _accountManager
            .GerVolunteerAccountByUserId(command.UserId, cancellationToken);
        if (volunteerAccount is null)
            return Errors.Accounts.VolunteerNotFound(command.UserId).ToErrorList();

        var newPaymentDetails = command.PaymentDetails
            .Select(pd => PaymentDetails.Create(pd.Name, pd.Description).Value).ToList();

        volunteerAccount.PaymentDetails = newPaymentDetails;

        await _unitOfWork.SaveChanges(cancellationToken);

        return UnitResult.Success<ErrorList>();
    }
}

