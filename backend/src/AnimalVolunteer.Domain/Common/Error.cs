namespace AnimalVolunteer.Domain.Common;

public record Error
{
    public const string SEPARATOR = "||";
    public const string PARSING_ERROR_TEXT = "Invalid serialized format";

    public string Code { get; }
    public string Message { get; }
    public ErrorType Type { get; }
    public string? InvalidField { get; } = null;

    private Error(
        string code, 
        string message, 
        ErrorType type, 
        string? invalidField = null)
    {
        Code = code;
        Message = message;
        Type = type;
        InvalidField = invalidField;
    }

    public static Error Validation(string code, string message, string? invalidField = null) =>
        new(code, message, ErrorType.Validation, invalidField);

    public static Error NotFound(string code, string message) =>
        new(code, message, ErrorType.NotFound);

    public static Error Failure(string code, string message) =>
        new(code, message, ErrorType.Failure);

    public static Error Unexpected(string code, string message) =>
        new(code, message, ErrorType.Unexpected);

    public static Error Conflict(string code, string message) =>
        new(code, message, ErrorType.Conflict);

    public string Serialize() => string.Join(SEPARATOR, Code, Message, Type);

    public static Error Deserialize(string serialized)
    {
        var parts = serialized.Split(SEPARATOR);

        if (parts.Length < 3)
            throw new ArgumentException(PARSING_ERROR_TEXT);

        if (Enum.TryParse<ErrorType>(parts[2], out var type) == false)
            throw new ArgumentException(PARSING_ERROR_TEXT);

        return new Error(parts[0], parts[1], type);
    }

    public ErrorList ToErrorList() => new([this]);

    public enum ErrorType
    {
        Validation,
        NotFound,
        Failure,
        Unexpected,
        Conflict
    }
}
