namespace AnimalVolunteer.SharedKernel;

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
        public static Error NonExistantSpeciesAndBreed(Guid speciesId, Guid breedId) =>
            Error.NotFound(
                "Pet.WrongSpecies&Breed",
                $"Non-existant pair of species id {speciesId} and breed id {breedId}");
    }

    public static class Species
    {
        public static Error NonExistantSpecies(Guid id) =>
            Error.NotFound("Species.NonExistantSpecies", $"Non-existant species (id = {id})");
        public static Error NonExistantBreed(Guid id) =>
            Error.NotFound("Species.NonExistantBreed", $"Non-existant breed (id = {id})");
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

    public static class Authentication
    {
        public static Error WrongCredentials() =>
            Error.NotFound("Auth.WrongCreds", "Failed to login using current credentials");

        public static Error RefreshExpired() =>
            Error.Failure("Auth.RefreshExpired", "Refresh token expired");

        public static Error InvalidToken() =>
            Error.Failure("Auth.InvalidToken", "Access token is invalid");

        public static Error IdMismatch() =>
            Error.Failure("Auth.IdMismatch", "User id from access token doesn't match with id in system");
    }

    public static class Accounts
    {
        public static Error RoleNotFound(string roleName) =>
            Error.NotFound("Accounts.RoleNotFound", $"Failed to find {roleName} role");

        public static Error UserNotFoud(Guid id) =>
            Error.NotFound("Accounts.UserNotFound", $"Failed to find user with id {id}");

        public static Error VolunteerNotFound(Guid id) =>
            Error.NotFound("Accounts.VolunteerNotFound", $"Failed to find volunteer account with userId {id}");

        public static Error RefreshSessionNotFound(Guid refreshToken) =>
            Error.NotFound("Accounts.RefreshSessionNotFound", 
                $"Failed to find refresh session by refresh token {refreshToken}");
    }

    public static class  Disscussions
    {
        public static Error InvalidDiscussionUsers() =>
            Error.Validation("Disscussions.InvalidDiscussionUsers", "Unable to create discussion: invalid user's ids");

        public static Error IncorrectUsersQuantity() =>
            Error.Validation("Disscussions.IncorrectUsersQuantity", 
                "Unable to create discussion with less or more than 2 users");

        public static Error MessagingNotAllowed(Guid userId, Guid discussionId) =>
            Error.Conflict("Discussions.MessagingNotAllowed", 
                $"User with id {userId} does not participate in discusstion with id {discussionId}");

        public static Error EmptyMessage() =>
            Error.Validation("Discussions.EmptyMessage", "Unable to create new message withot text");

        public static Error AccesDenied() =>
            Error.Conflict("Discussions.AccessDenied", "User can delete or amend only his own messages");

        public static Error MessageNotFound(Guid id) =>
            Error.NotFound("Discussions.MessageNotFound", $"Message with id {id} was not found in discussion");

        public static Error DiscussionClosed(Guid id) =>
            Error.Failure("Discussions.DiscussionClosed", 
                $"Discussion with id {id} closed, could not perform any actions with it");
    }

    public static class VolunteerRequests
    {
        public static Error RejectionMessageEmpty(Guid id) =>
            Error.Validation("VolunteerRequest.EmptyMessage", 
                $"Rejection message of volunteer request with id {id} could not be empty");

        public static Error WrongStatusChange() =>
            Error.Conflict("VolunteerRequest.WrongStatusChange", "Incorrect status shift sequence");

        public static Error AlreadyExist() =>
            Error.Conflict("VolunteerRequest.AlreadyExist", "One user can have only one volunteer request");
    }
}