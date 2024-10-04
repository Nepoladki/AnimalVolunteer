using AnimalVolunteer.Application.Database;
using AnimalVolunteer.Application.DTOs.Volunteer.Pet;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.AddPetPhotos;
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
using NSubstitute.ExceptionExtensions;
using System.Collections.Generic;

namespace AnimalVolunteer.Application.UnitTests;

public class AddPetPhotosHandlerTests
{
    private readonly IVolunteerRepository _volunteerRepository = Substitute
        .For<IVolunteerRepository>();

    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    private readonly IFileProvider _fileProvider = Substitute
        .For<IFileProvider>();

    private readonly ILogger<AddPetPhotosHandler> _logger = Substitute
        .For<ILogger<AddPetPhotosHandler>>();

    private readonly IValidator<AddPetPhotosCommand> _validator = Substitute
        .For<IValidator<AddPetPhotosCommand>>();

    private readonly IMessageQueue<IEnumerable<FileInfoDto>> _messageQueue = 
        Substitute.For<IMessageQueue<IEnumerable<FileInfoDto>>>();

    private readonly CancellationToken _cancellationToken = 
        new CancellationTokenSource().Token;

    [Fact]
    public async Task Handle_SuccsessfullAddedPhotos_ReturnsSuccess()
    {
        // Arrange
        var files = GetFilesToUpload(["TestName1.jpg", "TestName2.jpg"]);

        var command = GetCommand(files);

        var volunteerId = VolunteerId.CreateWithGuid(command.VolunteerId);

        var volunteer = GetVolunteer(volunteerId);
        AddPetWithId(volunteer, command.PetId);

        _volunteerRepository.GetById(volunteerId, _cancellationToken)
            .Returns(Result.Success<Volunteer, Error>(volunteer));
        
        _unitOfWork.SaveChanges(_cancellationToken).Returns(Task.CompletedTask);

        IReadOnlyList<FilePath> pathsList = 
        [ 
            FilePath.Create(Guid.NewGuid(), ".jpg").Value,
            FilePath.Create(Guid.NewGuid(), ".jpg").Value
        ];

        _fileProvider.UploadFiles(
            Arg.Any<IEnumerable<UploadingFileDto>>(), 
            Arg.Any<string>(), 
            _cancellationToken)
            .Returns(Result.Success<IReadOnlyList<FilePath>, Error>(pathsList));

        _validator.ValidateAsync(Arg.Any<AddPetPhotosCommand>(), _cancellationToken)
            .Returns(new ValidationResult());

        var handler = new AddPetPhotosHandler(
            _volunteerRepository, 
            _unitOfWork, 
            _fileProvider, 
            _logger, 
            _validator, 
            _messageQueue);

        // Act
        var result = await handler.Handle(command, _cancellationToken);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
    [Fact]
    public async Task Handle_FailValidation_ReturnsErrorList()
    {
        // Arrange
        var files = GetFilesToUpload(["invalidFileName1", "InvalidFileName2"]);

        var command = GetCommand(files);

        var volunteerId = VolunteerId.CreateWithGuid(command.VolunteerId);

        var volunteer = GetVolunteer(volunteerId);
        AddPetWithId(volunteer, command.PetId);

        _volunteerRepository.GetById(volunteerId, _cancellationToken)
            .Returns(Result.Success<Volunteer, Error>(volunteer));

        _unitOfWork.SaveChanges(_cancellationToken).Returns(Task.CompletedTask);

        IReadOnlyList<FilePath> pathsList =
        [
            FilePath.Create(Guid.NewGuid(), ".jpg").Value,
            FilePath.Create(Guid.NewGuid(), ".jpg").Value
        ];

        _fileProvider.UploadFiles(
            Arg.Any<IEnumerable<UploadingFileDto>>(),
            Arg.Any<string>(),
            _cancellationToken)
            .Returns(Result.Success<IReadOnlyList<FilePath>, Error>(pathsList));

        _validator.ValidateAsync(Arg.Any<AddPetPhotosCommand>(), _cancellationToken)
            .Returns(new ValidationResult(
                [new ValidationFailure("TestPropName", "500 || Prop || Validation")]));

        var handler = new AddPetPhotosHandler(
            _volunteerRepository,
            _unitOfWork, 
            _fileProvider, 
            _logger,
            _validator, 
            _messageQueue);

        // Act
        var result = await handler.Handle(command, _cancellationToken);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType(typeof(ErrorList));
    }

    [Fact]
    public async Task Handle_VolunteerNotFoundInRepository_ReturnsErrorList()
    {
        // Arrange
        var files = GetFilesToUpload(["TestName1.jpg", "TestName2.jpg"]);

        var command = GetCommand(files);

        var volunteerId = VolunteerId.CreateWithGuid(command.VolunteerId);

        _volunteerRepository.GetById(volunteerId, _cancellationToken)
            .Returns(
            Result.Failure<Volunteer, Error>(Errors.General.NotFound(volunteerId)));

        _unitOfWork.SaveChanges(_cancellationToken).Returns(Task.CompletedTask);

        IReadOnlyList<FilePath> pathsList =
        [
            FilePath.Create(Guid.NewGuid(), ".jpg").Value,
            FilePath.Create(Guid.NewGuid(), ".jpg").Value
        ];

        _fileProvider.UploadFiles(
            Arg.Any<IEnumerable<UploadingFileDto>>(),
            Arg.Any<string>(),
            _cancellationToken)
            .Returns(Result.Success<IReadOnlyList<FilePath>, Error>(pathsList));

        _validator.ValidateAsync(Arg.Any<AddPetPhotosCommand>(), _cancellationToken)
            .Returns(new ValidationResult());

        var handler = new AddPetPhotosHandler(
            _volunteerRepository, 
            _unitOfWork, 
            _fileProvider, 
            _logger, 
            _validator, 
            _messageQueue);

        // Act
        var result = await handler.Handle(command, _cancellationToken);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType(typeof(ErrorList));
    }

    [Fact]
    public async Task Handle_PetNotFound_ReturnsErrorList()
    {
        // Arrange
        var files = GetFilesToUpload(["TestName1.jpg", "TestName2.jpg"]);

        var command = GetCommand(files);

        var volunteerId = VolunteerId.CreateWithGuid(command.VolunteerId);

        var volunteer = GetVolunteer(volunteerId);

        _volunteerRepository.GetById(volunteerId, _cancellationToken)
            .Returns(Result.Success<Volunteer, Error>(volunteer));

        _unitOfWork.SaveChanges(_cancellationToken).Returns(Task.CompletedTask);

        IReadOnlyList<FilePath> pathsList = [
            FilePath.Create(Guid.NewGuid(), ".jpg").Value,
            FilePath.Create(Guid.NewGuid(), ".jpg").Value];

        _fileProvider.UploadFiles(
            Arg.Any<IEnumerable<UploadingFileDto>>(),
            Arg.Any<string>(),
            _cancellationToken)
            .Returns(Result.Success<IReadOnlyList<FilePath>, Error>(pathsList));

        _validator.ValidateAsync(Arg.Any<AddPetPhotosCommand>(), _cancellationToken)
            .Returns(new ValidationResult());

        var handler = new AddPetPhotosHandler(
            _volunteerRepository,
            _unitOfWork,
            _fileProvider,
            _logger,
            _validator,
            _messageQueue);

        // Act
        var result = await handler.Handle(command, _cancellationToken);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType(typeof(ErrorList));
    }

    [Fact]
    public async Task Handle_FileProviderError_ReturnsErrorList()
    {
        // Arrange
        var files = GetFilesToUpload(["TestName1.jpg", "TestName2.jpg"]);

        var command = GetCommand(files);

        var volunteerId = VolunteerId.CreateWithGuid(command.VolunteerId);

        var volunteer = GetVolunteer(volunteerId);
        AddPetWithId(volunteer, command.PetId);

        _volunteerRepository.GetById(volunteerId, _cancellationToken)
            .Returns(Result.Success<Volunteer, Error>(volunteer));

        _unitOfWork.SaveChanges(_cancellationToken).Returns(Task.CompletedTask);

        _fileProvider.UploadFiles(
            Arg.Any<IEnumerable<UploadingFileDto>>(),
            Arg.Any<string>(),
            _cancellationToken)
            .Returns(Result.Failure<IReadOnlyList<FilePath>, Error>(
                Error.Failure("file.upload", "Failed to upload file in Minio")));

        _validator.ValidateAsync(Arg.Any<AddPetPhotosCommand>(), _cancellationToken)
            .Returns(new ValidationResult());

        _messageQueue.WriteAsync(Arg.Any<IEnumerable<FileInfoDto>>(), _cancellationToken)
            .Returns(Task.CompletedTask);

        var handler = new AddPetPhotosHandler(
            _volunteerRepository,
            _unitOfWork,
            _fileProvider,
            _logger,
            _validator,
            _messageQueue);

        // Act
        var result = await handler.Handle(command, _cancellationToken);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType(typeof(ErrorList));
        result.Error.First().Code.Should().BeSameAs("file.upload");
    }

    [Fact]
    public async Task Handle_ExceptionInTryCatch_ReturnsErrorList()
    {
        // Arrange
        var files = GetFilesToUpload(["TestName1.jpg", "TestName2.jpg"]);

        var command = GetCommand(files);

        var volunteerId = VolunteerId.CreateWithGuid(command.VolunteerId);

        var volunteer = GetVolunteer(volunteerId);
        AddPetWithId(volunteer, command.PetId);

        _volunteerRepository.GetById(volunteerId, _cancellationToken)
            .Returns(Result.Success<Volunteer, Error>(volunteer));

        _unitOfWork.SaveChanges(_cancellationToken)
            .Throws(new Exception("TestException"));

        IReadOnlyList<FilePath> pathsList = [
            FilePath.Create(Guid.NewGuid(), ".jpg").Value,
            FilePath.Create(Guid.NewGuid(), ".jpg").Value];

        _fileProvider.UploadFiles(
            Arg.Any<IEnumerable<UploadingFileDto>>(),
            Arg.Any<string>(),
            _cancellationToken)
            .Returns(Result.Success<IReadOnlyList<FilePath>, Error>(pathsList));

        _validator.ValidateAsync(Arg.Any<AddPetPhotosCommand>(), _cancellationToken)
            .Returns(new ValidationResult());

        var handler = new AddPetPhotosHandler(
            _volunteerRepository,
            _unitOfWork,
            _fileProvider,
            _logger,
            _validator,
            _messageQueue);

        // Act
        var result = await handler.Handle(command, _cancellationToken);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType(typeof(ErrorList));
        result.Error.First().Code.Should().BeSameAs("volunteer.pet.photos.failure");
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
    
    private static List<UploadFileDto> GetFilesToUpload(params string[] filenames)
    {
        List<UploadFileDto> files = [];

        foreach (var name in filenames)
            files.Add(new UploadFileDto(name, new MemoryStream()));
        
        return files;
    }

    private static AddPetPhotosCommand GetCommand(List<UploadFileDto> files)
    {
        return new AddPetPhotosCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            files);
    }
}
