using AnimalVolunteer.Domain.Aggregates.Volunteer.Entities;
using AnimalVolunteer.Domain.Aggregates.Volunteer.Enums;
using AnimalVolunteer.Domain.Aggregates.Volunteer.Root;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Pet;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Volunteer;
using AnimalVolunteer.Domain.Common.ValueObjects;
using FluentAssertions;

namespace AnimalVolunteer.Domain.UnitTests;

public class VolunteerTests
{
    [Fact]
    public void AddPet_ToEmptyPetList_ReturnsOne()
    {
        // Arrange
        Volunteer volunteer = CreateVolunteer();
        Pet pet = CreatePet();

        // Act
        var result = volunteer.AddPet(pet);

        // Assert
        var addedPet = volunteer.GetPetById(pet.Id).Value;
        var position = addedPet.Position.Value;

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Equal(1, position);
    }

    [Fact]
    public void MovePet_ShouldNotMove_WhenPetAlreadyAtNewPosition()
    {
        // Arrange
        const int petCount = 5;
        
        var volunteer = CreateVolunteerWithPets(petCount);
     
        var secondPosition = Position.Create(2).Value;

        var firstPet = volunteer.Pets[0];
        var secondPet = volunteer.Pets[1];
        var thirdPet = volunteer.Pets[2];
        var fourthPet = volunteer.Pets[3];
        var fifthPet = volunteer.Pets[4];


        // Act
        var result = volunteer.MovePet(secondPet, secondPosition);

        // Assert
        result.IsSuccess.Should().BeTrue();
        firstPet.Position.Value.Should().Be(1);
        secondPet.Position.Value.Should().Be(2);
        thirdPet.Position.Value.Should().Be(3);
        fourthPet.Position.Value.Should().Be(4);
        fifthPet.Position.Value.Should().Be(5);
    }

    [Fact]
    public void MovePet_ForwardRearrangement_WhenNewPositionLower()
    {
        // Arrange
        const int petCount = 5;

        var volunteer = CreateVolunteerWithPets(petCount);

        var secondPosition = Position.Create(2).Value;

        var firstPet = volunteer.Pets[0];
        var secondPet = volunteer.Pets[1];
        var thirdPet = volunteer.Pets[2];
        var fourthPet = volunteer.Pets[3];
        var fifthPet = volunteer.Pets[4];

        // Act
        var result = volunteer.MovePet(fourthPet, secondPosition);

        // Assert
        result.IsSuccess.Should().BeTrue();
        firstPet.Position.Value.Should().Be(1);
        secondPet.Position.Value.Should().Be(3);
        thirdPet.Position.Value.Should().Be(4);
        fourthPet.Position.Value.Should().Be(2);
        fifthPet.Position.Value.Should().Be(5);
    }

    [Fact]
    public void MovePet_BackwardRearrangement_WhenNewPositionHigher()
    {   // 1 2 3 4 5 => 1 3 4 2 5
        // Arrange
        const int petCount = 5;

        var volunteer = CreateVolunteerWithPets(petCount);

        var fourthPosition = Position.Create(4).Value;

        var firstPet = volunteer.Pets[0];
        var secondPet = volunteer.Pets[1];
        var thirdPet = volunteer.Pets[2];
        var fourthPet = volunteer.Pets[3];
        var fifthPet = volunteer.Pets[4];

        // Act
        var result = volunteer.MovePet(secondPet, fourthPosition);

        // Assert
        result.IsSuccess.Should().BeTrue();
        firstPet.Position.Value.Should().Be(1);
        secondPet.Position.Value.Should().Be(4);
        thirdPet.Position.Value.Should().Be(2);
        fourthPet.Position.Value.Should().Be(3);
        fifthPet.Position.Value.Should().Be(5);
    }
    [Fact]
    public void MovePet_MoveAllForward_WhenNewPositionIsFirst()
    {   // 1 2 3 4 5 =>  5 1 2 3 4
        // Arrange
        const int petCount = 5;

        var volunteer = CreateVolunteerWithPets(petCount);

        var firstPosition = Position.Create(1).Value;

        var firstPet = volunteer.Pets[0];
        var secondPet = volunteer.Pets[1];
        var thirdPet = volunteer.Pets[2];
        var fourthPet = volunteer.Pets[3];
        var fifthPet = volunteer.Pets[4];

        // Act
        var result = volunteer.MovePet(fifthPet, firstPosition);

        // Assert
        result.IsSuccess.Should().BeTrue();
        firstPet.Position.Value.Should().Be(2);
        secondPet.Position.Value.Should().Be(3);
        thirdPet.Position.Value.Should().Be(4);
        fourthPet.Position.Value.Should().Be(5);
        fifthPet.Position.Value.Should().Be(1);
    }
    [Fact]
    public void MovePet_MoveAllBackward_WhenNewPositionIsLast()
    {   // 1 2 3 4 5 => 2 3 4 5 1
        // Arrange
        const int petCount = 5;

        var volunteer = CreateVolunteerWithPets(petCount);

        var lastPosition = Position.Create(5).Value;

        var firstPet = volunteer.Pets[0];
        var secondPet = volunteer.Pets[1];
        var thirdPet = volunteer.Pets[2];
        var fourthPet = volunteer.Pets[3];
        var fifthPet = volunteer.Pets[4];

        // Act
        var result = volunteer.MovePet(firstPet, lastPosition);

        // Assert
        result.IsSuccess.Should().BeTrue();
        firstPet.Position.Value.Should().Be(5);
        secondPet.Position.Value.Should().Be(1);
        thirdPet.Position.Value.Should().Be(2);
        fourthPet.Position.Value.Should().Be(3);
        fifthPet.Position.Value.Should().Be(4);
    }
    private Volunteer CreateVolunteer()
    {
        var id = VolunteerId.Create();
        var fullName = FullName.Create("Test", "Test", "Test").Value;
        var email = Email.Create("testtest@mail.ru").Value;
        var description = Description.Create("TestDesc").Value;
        var statistics = Statistics.Create(1, 1, 1, 1).Value;
        var contacts = ContactInfoList.CreateEmpty();
        var socials = SocialNetworkList.CreateEmpty();
        var payment = PaymentDetailsList.CreateEmpty();

        return Volunteer.Create(
            id,
            fullName,
            email,
            description,
            statistics,
            contacts,
            socials,
            payment);
    }

    private Pet CreatePet()
    {
        var id = PetId.Create();
        var name = Name.Create("TestPet").Value;
        var description = Description.Create("TestDesc").Value;
        var physicalParameters = PhysicalParameters.Create("test", 1, 1).Value;
        var speciesAndBreed = SpeciesAndBreed.Create(Guid.NewGuid(), Guid.NewGuid()).Value;
        var healthInfo = HealthInfo.Create("test", true, true).Value;
        var address = Address.Create("test", "test", "test", "test").Value;
        var birthDate = DateOnly.MinValue;
        var status = CurrentStatus.LookingForHelp;

        var pet = Pet.InitialCreate(
            id,
            name,
            description,
            physicalParameters,
            speciesAndBreed,
            healthInfo,
            address,
            birthDate,
            status);
        return pet;
    }

    private Volunteer CreateVolunteerWithPets(int petCount)
    {
        var volunteer = CreateVolunteer();

        for (int i = 0; i < petCount; i++) {
            var pet = CreatePet();
            volunteer.AddPet(pet);
        }

        return volunteer;
    }
}