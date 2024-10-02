﻿using AnimalVolunteer.Application.Interfaces;
using DomainEntity = AnimalVolunteer.Domain.Aggregates.Volunteer;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Volunteer;
using CSharpFunctionalExtensions;
using AnimalVolunteer.Application.Database;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Common.ValueObjects;
using FluentValidation;
using AnimalVolunteer.Application.Extensions;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Create;

public class CreateVolunteerHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IValidator<CreateVolunteerCommand> _validator;

    public CreateVolunteerHandler(
        IVolunteerRepository volunteerRepository,
        IValidator<CreateVolunteerCommand> validator)
    {
        _volunteerRepository = volunteerRepository;
        _validator = validator;
    }
    public async Task<Result<VolunteerId, ErrorList>> Create(
        CreateVolunteerCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator
            .ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var email = Email.Create(command.Email).Value;

        if (await _volunteerRepository.ExistByEmail(email, cancellationToken))
            return Errors.Volunteer.AlreadyExist().ToErrorList();

        var fullName = FullName.Create(
           command.FullName.FirstName,
           command.FullName.SurName,
           command.FullName.LastName).Value;

        var description = Description.Create(command.Description).Value;

        var statistics = Statistics.CreateEmpty();

        var contactInfo = ContactInfoList.CreateEmpty();

        var socialNetworks = SocialNetworkList.Create(command.SocialNetworkList
            .Select(x => SocialNetwork.Create(x.Name, x.URL).Value));

        var paymentDetails = PaymentDetailsList.Create(command.PaymentDetailsList
            .Select(x => PaymentDetails.Create(x.Name, x.Description).Value));

        var volunteer = DomainEntity.Root.Volunteer.Create(
            VolunteerId.Create(),
            fullName,
            email,
            description,
            statistics,
            contactInfo,
            socialNetworks,
            paymentDetails);

        await _volunteerRepository.Create(volunteer, cancellationToken);

        return volunteer.Id;
    }
}