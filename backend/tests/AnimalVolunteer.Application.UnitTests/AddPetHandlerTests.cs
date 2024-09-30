using AnimalVolunteer.Application.Database;
using AnimalVolunteer.Application.Features.Volunteer.AddPet;
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

namespace AnimalVolunteer.Application.UnitTests
{
    public class AddPetHandlerTests
    {
        [Fact]
        public async Task Handle_VolunteerDoesntExist_ReturnsError()
        {
            // Arrange
            var ct = new CancellationTokenSource().Token;

            var volunteerId = VolunteerId.Create();

            var command = GetAddPetCommand(volunteerId);

            var volunteerRepository = Substitute.For<IVolunteerRepository>();

            volunteerRepository.GetById(volunteerId, ct)
                .ReturnsForAnyArgs(Errors.General.NotFound(volunteerId));

            var logger = Substitute.For<ILogger<AddPetHandler>>();

            var unitOfWork = Substitute.For<IUnitOfWork>();

            var validator = Substitute.For<IValidator<AddPetCommand>>();

            validator.ValidateAsync(command, ct).Returns(new ValidationResult());

            var handler = new AddPetHandler(
                volunteerRepository,
                logger,
                unitOfWork,
                validator);

            // Act
            var result = await handler.Handle(command, ct);

            // Assert
            result.Error.Should().Equal(Errors.General.NotFound(volunteerId));
        }

        [Fact]
        public async Task Handle_UnsuccesfullValidation_ReturnsError()
        {
            // Arrange
            var ct = new CancellationTokenSource().Token;

            var volunteerId = VolunteerId.Create();

            var command = GetAddPetCommand(volunteerId);

            var volunteerRepository = Substitute.For<IVolunteerRepository>();

            var logger = Substitute.For<ILogger<AddPetHandler>>();

            var unitOfWork = Substitute.For<IUnitOfWork>();

            var validator = Substitute.For<IValidator<AddPetCommand>>();

            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("TestPropName", "500 || Prop || Validation")
            };

            validator.ValidateAsync(command, ct).Returns(new ValidationResult(validationFailures));

            var handler = new AddPetHandler(
                volunteerRepository,
                logger,
                unitOfWork,
                validator);

            // Act
            var result = await handler.Handle(command, ct);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().BeOfType(typeof(ErrorList));
        }

        [Fact]
        public async Task Handle_Succesfull_ReturnsGuid()
        {
            // Arrange
            var ct = new CancellationTokenSource().Token;

            var volunteerId = VolunteerId.Create();

            var command = GetAddPetCommand(volunteerId);

            var volunteerRepository = Substitute.For<IVolunteerRepository>();

            volunteerRepository.GetById(volunteerId, ct)
                .Returns(Result.Success<Volunteer, Error>(GetVolunteer(volunteerId)));

            var logger = Substitute.For<ILogger<AddPetHandler>>();

            var unitOfWork = Substitute.For<IUnitOfWork>();

            unitOfWork.SaveChanges().Returns(Task.CompletedTask);

            var validator = Substitute.For<IValidator<AddPetCommand>>();

            validator.ValidateAsync(command, ct).Returns(new ValidationResult());

            var handler = new AddPetHandler(
                volunteerRepository,
                logger,
                unitOfWork,
                validator);

            // Act
            var result = await handler.Handle(command, ct);

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
            var contacts = ContactInfoList.CreateEmpty();
            var payments = PaymentDetailsList.CreateEmpty();
            var socials = SocialNetworkList.CreateEmpty();

            return Volunteer.Create(
                id,
                fullName,
                email,
                description,
                statistics,
                contacts,
                socials,
                payments);
        }
    }
}