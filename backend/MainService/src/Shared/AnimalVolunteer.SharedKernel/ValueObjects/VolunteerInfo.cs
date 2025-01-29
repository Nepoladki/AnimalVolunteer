using CSharpFunctionalExtensions;

namespace AnimalVolunteer.SharedKernel.ValueObjects;

public record VolunteerInfo
{
    private VolunteerInfo(string expirienceDescription, string passport)
    {
        ExpirienceDescription = expirienceDescription;
        Passport = passport;
    }
    public string ExpirienceDescription { get; private set; } = default!;
    public string Passport { get; private set; } = default!;

    public static Result<VolunteerInfo, Error> Create(string expirienceDescription, string passport)
    {
        if (string.IsNullOrWhiteSpace(expirienceDescription))
            return Errors.General.InvalidValue(nameof(expirienceDescription));

        if (string.IsNullOrWhiteSpace(passport))
            return Errors.General.InvalidValue(nameof(passport));

        return new VolunteerInfo(expirienceDescription, passport);
    }
}

