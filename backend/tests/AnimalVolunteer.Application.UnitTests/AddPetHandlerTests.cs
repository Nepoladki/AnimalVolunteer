using AnimalVolunteer.Application.Database;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Pet.AddPet;
using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Aggregates.Volunteer.Enums;
using AnimalVolunteer.Domain.Aggregates.Volunteer.Root;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Volunteer;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Common.ValueObjects;
using CSharpFunctionalExtensions;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace AnimalVolunteer.Application.UnitTests;

public class AddPetHandlerTests
{
    private readonly IVolunteerRepository _volunteerRepository = Substitute
        .For<IVolunteerRepository>();

    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    private readonly ILogger<AddPetHandler> _logger = Substitute
        .For<ILogger<AddPetHandler>>();

    private readonly IValidator<AddPetCommand> _validator = Substitute
        .For<IValidator<AddPetCommand>>();

    private readonly CancellationToken _cancellationToken =
        new CancellationTokenSource().Token;

    [Fact]
    public async Task Handle_VolunteerDoesntExist_ReturnsError()
    {
        // Arrange
        var volunteerId = VolunteerId.Create();

        var command = GetAddPetCommand(volunteerId);

        _volunteerRepository.GetById(volunteerId, _cancellationToken)
            .ReturnsForAnyArgs(Errors.General.NotFound(volunteerId));

        _validator.ValidateAsync(command, _cancellationToken)
            .Returns(new ValidationResult());

        var handler = new AddPetHandler(
            _volunteerRepository,
            _logger,
            _unitOfWork,
            _validator);

        // Act
        var result = await handler.Handle(command, _cancellationToken);

        // Assert
        result.Error.Should().Equal(Errors.General.NotFound(volunteerId));
    }

    [Fact]
    public async Task Handle_UnsuccesfullValidation_ReturnsError()
    {
        // Arrange
        var volunteerId = VolunteerId.Create();

        var command = GetAddPetCommand(volunteerId);

        var validationFailures = new List<ValidationFailure>
        {
            new ValidationFailure("TestPropName", "500 || Prop || Validation")
        };

        _validator.ValidateAsync(command, _cancellationToken)
            .Returns(new ValidationResult(validationFailures));

        var handler = new AddPetHandler(
            _volunteerRepository,
            _logger,
            _unitOfWork,
            _validator);

        // Act
        var result = await handler.Handle(command, _cancellationToken);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().BeOfType(typeof(ErrorList));
    }

    [Fact]
    public async Task Handle_Succesfull_ReturnsGuid()
    {
        // Arrange
        var volunteerId = VolunteerId.Create();

        var command = GetAddPetCommand(volunteerId);

        _volunteerRepository.GetById(volunteerId, _cancellationToken)
            .Returns(Result.Success<Volunteer, Error>(GetVolunteer(volunteerId)));

        _unitOfWork.SaveChanges().Returns(Task.CompletedTask);

        _validator.ValidateAsync(command, _cancellationToken).Returns(new ValidationResult());

        var handler = new AddPetHandler(
            _volunteerRepository,
            _logger,
            _unitOfWork,
            _validator);

        // Act
        var result = await handler.Handle(command, _cancellationToken);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(volunteerId);
    }

    private AddPetCommand GetAddPetCommand(VolunteerId id)
    {
        return new AddPetCommand(
            id,
            "TestName",
            "TestDesc",
            "TestColor",
            1,
            1,
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Test",
            true,
            true,
            "Test",
            "Test",
            "Test",
            "Test",
            DateOnly.MaxValue,
            CurrentStatus.LookingForHelp);
    }

    private Volunteer GetVolunteer(VolunteerId id)
    {
        var fullName = FullName.Create("Test", "Test", "Test").Value;
        var email = Email.Create("Testmail@mail.ru").Value;
        var description = Description.Create("Test").Value;
        var statistics = Statistics.Create(1, 1, 1, 1).Value;
        var payments = new List<PaymentDetails>();
        var socials = new List<SocialNetwork>();

        return Volunteer.Create(
            id,
            fullName,
            email,
            description,
            statistics,
            socials,
            payments);
    }
}