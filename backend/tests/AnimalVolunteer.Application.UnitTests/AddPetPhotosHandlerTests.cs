using AnimalVolunteer.Application.Database;
using AnimalVolunteer.Application.DTOs.Volunteer.Pet;
using AnimalVolunteer.Application.Features.Volunteer.AddPet;
using AnimalVolunteer.Application.Features.Volunteer.AddPetPhotos;
using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Aggregates.Volunteer.Entities;
using AnimalVolunteer.Domain.Aggregates.Volunteer.Enums;
using AnimalVolunteer.Domain.Aggregates.Volunteer.Root;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Pet;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Volunteer;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Common.ValueObjects;
using CSharpFunctionalExtensions;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Data.Common;

namespace AnimalVolunteer.Application.UnitTests;

public class AddPetPhotosHandlerTests
{
    [Fact]
    public async Task Handle_SuccsessfullAddedPhotos_ReturnsSuccess()
    {
        // Arrange
        var ct = new CancellationTokenSource().Token;

        var files = new List<UploadFileDto>
        {
            new UploadFileDto("TestName.jpg", new MemoryStream()),
            new UploadFileDto("TestName2.jpg", new MemoryStream())
        };

        var command = new AddPetPhotosCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            files);

        var volunteerId = VolunteerId.CreateWithGuid(command.VolunteerId);

        var volunteer = GetVolunteer(volunteerId);
        AddPetWithId(volunteer, command.PetId);

        var repository = Substitute.For<IVolunteerRepository>();
        repository.GetById(volunteerId, ct)
            .Returns(Result.Success<Volunteer, Error>(volunteer));
        
        var unitOfWork = Substitute.For<IUnitOfWork>();
        unitOfWork.SaveChanges(ct).Returns(Task.CompletedTask);

        IReadOnlyList<FilePath> pathsList = 
        [ 
            FilePath.Create(Guid.NewGuid(), ".jpg").Value,
            FilePath.Create(Guid.NewGuid(), ".jpg").Value
        ];

        var fileProvider = Substitute.For<IFileProvider>();
        fileProvider.UploadFiles(
            Arg.Any<IEnumerable<UploadingFileDto>>(), 
            Arg.Any<string>(), 
            ct)
            .Returns(Result.Success<IReadOnlyList<FilePath>, Error>(pathsList));


        var logger = Substitute.For<ILogger<AddPetPhotosHandler>>();

        var validator = Substitute.For<IValidator<AddPetPhotosCommand>>();
        validator.ValidateAsync(Arg.Any<AddPetPhotosCommand>(), ct)
            .Returns(new ValidationResult());

        var handler = new AddPetPhotosHandler(
            repository, unitOfWork, fileProvider, logger, validator);

        // Act
        var result = await handler.Handle(command, ct);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
    [Fact]
    public async Task Handle_FailedValidation_ReturnsErrorList()
    {

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
    private void AddPetWithId(Volunteer volunteer, PetId id)
    {
        var name = Name.Create("TestName").Value;
        var description = Description.Create("TestDesc").Value;
        var physicalParameters = PhysicalParameters
            .Create("TestColor", 1, 1).Value;
        var speciesAndBrees = SpeciesAndBreed
            .Create(Guid.NewGuid(), Guid.NewGuid()).Value;
        var healthInfo = HealthInfo.Create("Test", true, true).Value;
        var address = Address.Create("Test", "Test", "Test", "Test").Value;
        var date = DateOnly.MaxValue;
        var status = CurrentStatus.LookingForHelp;

        var pet = Pet.InitialCreate(
            id,
            name,
            description,
            physicalParameters,
            speciesAndBrees,
            healthInfo,
            address,
            date,
            status);

        volunteer.AddPet(pet);
    }
}
