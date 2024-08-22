namespace AnimalVolunteer.Domain.Errors;

public static partial class Errors
{
    public static class Volunteer
    {
        public static Error AlreadyExist() =>        
            Error.Validation("Volunteer.AlreadyExist", $"Volunteer Already Exist");
    }
}
