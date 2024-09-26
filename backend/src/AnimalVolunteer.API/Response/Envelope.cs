using AnimalVolunteer.Domain.Common;

namespace AnimalVolunteer.API.Response;

public record Envelope
{
    public object? Result { get; }
    public ErrorList? Errors { get; }
    public DateTime TimeGenerated { get; }
    private Envelope(object? result, ErrorList? error)
    {
        Result = result;
        Errors = error;
        TimeGenerated = DateTime.Now;
    }

    public static Envelope Ok(object? result = null) => new(result, null);
    public static Envelope Error(ErrorList errors) => new(null, errors);
}
