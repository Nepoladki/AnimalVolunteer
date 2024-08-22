namespace AnimalVolunteer.Domain.Errors;

public static partial class Errors
{
    public static class General
    {
        public static Error ValueIsInvalid(string? name = null)
        {
            var label = name ?? "Value";

            return Error.Validation("Invalid.Value", $"{label} is invalid");
        }

        public static Error NotFound(Guid? id = null)
        {
            var forId = id == null ? " " : $" for Id {id} ";

            return Error.NotFound("Record.NotFound", $"Record{forId}not found");
        }

        public static Error WrongValueLength(string? name = null)
        {
            var label = name == null ? " " : $"{ name }";

            return Error.Validation("Invalid.Value.Length", $"Invalid{label}length");
        }
    }
}
