﻿namespace AnimalVolunteer.Domain.Common;

public static class Errors
{
    public static class General
    {
        public static Error InvalidValue(string? name = null)
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
            var label = name == null ? " " : $"{name}";

            return Error.Validation("Invalid.Value.Length", $"Invalid{label}length");
        }

    }
    public static class Volunteer
    {
        public static Error AlreadyExist() =>
            Error.Validation("Volunteer.AlreadyExist", $"Volunteer Already Exist");
    }

    public static class Minio
    {
        public static Error BucketNotFound(string name) =>
            Error.Failure("Minio.BucketNotFound", $"Tried to access unexisting minio bucket {name}");

        public static Error GetUrlFailure(string name) =>
            Error.Failure("Minio.GetUrlFailure", $"Failed to fetch {name} from Minio");

        public static Error DeleteFailure(string name) =>
            Error.Failure("Minio.DeleteFailure", $"Failed to delete {name} from Minio");
    }
}