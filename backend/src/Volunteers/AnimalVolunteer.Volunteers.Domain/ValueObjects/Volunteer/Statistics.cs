using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;
using System.Reflection.Metadata.Ecma335;

namespace AnimalVolunteer.Volunteers.Domain.ValueObjects.Volunteer;
public record Statistics
{
    private const int MIN_VALUE = 0;
    private Statistics(
        int expirienceYears,
        int petsFoundedHome,
        int petsLookingForHome,
        int petsInVetClinic)
    {
        ExpirienceYears = expirienceYears;
        PetsFoundedHome = petsFoundedHome;
        PetsLookingForHome = petsLookingForHome;
        PetsInVetClinic = petsInVetClinic;
    }
    public int ExpirienceYears { get; }
    public int PetsFoundedHome { get; }
    public int PetsLookingForHome { get; }
    public int PetsInVetClinic { get; }
    public static Result<Statistics, Error> Create(
        int expirienceYears,
        int petsFoundedHome,
        int petsLookingForHome,
        int petsInVetClinic)
    {
        if (expirienceYears < MIN_VALUE)
            return Errors.General.InvalidValue(nameof(expirienceYears));

        if (petsFoundedHome < MIN_VALUE)
            return Errors.General.InvalidValue(nameof(petsFoundedHome));

        if (petsLookingForHome < MIN_VALUE)
            return Errors.General.InvalidValue(nameof(petsLookingForHome));

        if (petsInVetClinic < MIN_VALUE)
            return Errors.General.InvalidValue(nameof(petsInVetClinic));

        return new Statistics(
            expirienceYears,
            petsFoundedHome,
            petsLookingForHome,
            petsInVetClinic);
    }
    public static Statistics CreateEmpty() => new(0, 0, 0, 0);
}
