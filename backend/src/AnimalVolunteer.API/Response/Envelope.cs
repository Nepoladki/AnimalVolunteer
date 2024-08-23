using AnimalVolunteer.Domain.Common;
using System.Runtime.InteropServices;

namespace AnimalVolunteer.API.Response;

public record Envelope
{
    public object? Result { get; }
    public string? ErrorCode { get; }
    public string? ErrorMessage { get; }
    public DateTime TimeGenerated { get; } = DateTime.UtcNow;
    private Envelope(object? result, Error? error)
    {
        Result = result;
        ErrorCode = error?.Code;
        ErrorMessage = error?.Message;
    }

    public static Envelope Ok(object? result = null) => new(result, null);
    public static Envelope Error(Error error) => new(null, error);
}
