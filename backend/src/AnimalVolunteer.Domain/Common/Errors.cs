namespace AnimalVolunteer.Domain.Common;

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
            var label = name == null ? "" : $"{name}";

            return Error.Validation("Invalid.Value.Length", $"Invalid {label} length");
        }

        public static Error DeleteingConflict(Guid? id = null, string? entityTypeName = null)
        {
            var forId = id is null ? " " : $"with id {id} ";
            var type = entityTypeName is null ? "" : $"of type {entityTypeName}";

            return Error.Conflict("Conflict.Constraint", $"Can't delete entity {forId}{type}");
        }
    }

    public static class Volunteer
    {
        public static Error AlreadyExist() =>
            Error.Validation("Volunteer.AlreadyExist", "Volunteer Already Exist");
        public static Error PetNotFound(Guid volunteerId, Guid petId) =>
            Error.NotFound("Pet.NotFound", 
                $"Volunteer with id {volunteerId} does not have pet with id {petId}");
        public static Error PetStatusRestriction(Guid volunteerId) =>
            Error.Conflict("Restricted.PetStatus", 
                $"Volunteer (id = {volunteerId}) can not change pet status to FoundHome");
    }

    public static class Pet
    {
        public static Error NonExistantSpecies(Guid id) =>
            Error.NotFound("Pet.WrongSpecies", $"Unable to create new pet with species id {id}");
        public static Error NonExistantBreed(Guid id) =>
            Error.NotFound("Pet.WrongBreed", $"Unable to create new pet with breed id {id}");
        public static Error NonExistantSpeciesAndBreed(Guid speciesId, Guid breedId) =>
            Error.NotFound(
                "Pet.WrongSpecies&Breed", 
                $"Non-existant pair of species id {speciesId} and breed id {breedId}");
    }
    
    public static class Species
    {
        public static Error BreedDeletingError(Guid id) =>
            Error.Unexpected("Breed.DeletingFail", $"Failed to delete breed with id {id}");
    }

    public static class Minio
    {
        public static Error BucketNotFound(string name) =>
            Error.NotFound("Minio.BucketNotFound", $"Tried to access unexisting minio bucket {name}");

        public static Error ObjectNotFound(string name) =>
            Error.NotFound("Minio.ObjectNotFound", $"Tried to access unexisting minio object {name}");

        public static Error GetUrlFailure(string name) =>
            Error.Failure("Minio.GetUrlFailure", $"Failed to fetch {name} from Minio");

        public static Error DeleteFailure(string name) =>
            Error.Failure("Minio.DeleteFailure", $"Failed to delete {name} from Minio");
    }
}