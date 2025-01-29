using DomainEntity = AnimalVolunteer.Volunteers.Domain.Root;
using CSharpFunctionalExtensions;
using FluentValidation;
using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.Volunteers.Domain.ValueObjects.Volunteer;
using AnimalVolunteer.SharedKernel.ValueObjects;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.Volunteers.Application.Interfaces;

namespace AnimalVolunteer.Volunteers.Application.Commands.Volunteer.Create;

public class CreateVolunteerHandler : ICommandHandler<Guid, CreateVolunteerCommand>
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
    public async Task<Result<Guid, ErrorList>> Handle(
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
           command.FullName.Patronymic,
           command.FullName.LastName).Value;

        var description = Description.Create(command.Description).Value;

        var statistics = Statistics.CreateEmpty();

        var volunteer = DomainEntity.Volunteer.Create(
            VolunteerId.Create(),
            fullName,
            email,
            description,
            statistics);

        await _volunteerRepository.Create(volunteer, cancellationToken);

        return (Guid)volunteer.Id;
    }
}
