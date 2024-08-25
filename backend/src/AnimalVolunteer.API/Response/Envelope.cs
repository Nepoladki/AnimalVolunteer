namespace AnimalVolunteer.API.Response;

public record ResponseError(string? ErrorCode, string? ErrorMessage, string? InvalidField);
public record Envelope
{
    public object? Result { get; }
    public List<ResponseError> Errors { get; }
    public DateTime TimeGenerated { get; }
    private Envelope(object? result, IEnumerable<ResponseError> error)
    {
        Result = result;
        Errors = error.ToList();
        TimeGenerated = DateTime.Now;
    }

    public static Envelope Ok(object? result = null) => new(result, []);
    public static Envelope Error(IEnumerable<ResponseError>error) => new(null, error);
}
