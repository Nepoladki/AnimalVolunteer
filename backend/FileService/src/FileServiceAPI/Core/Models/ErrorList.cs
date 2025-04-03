using System.Collections;

namespace FileServiceAPI.Core.Models;

public class ErrorList : IEnumerable<Error>
{
    private readonly List<Error> _errors = [];
    public ErrorList(IEnumerable<Error> errors) =>
        _errors = errors.ToList();

    public IEnumerator<Error> GetEnumerator() => _errors.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public static implicit operator ErrorList(List<Error> errors)
        => new(errors);

    public static implicit operator ErrorList(Error error)
        => new([error]);
}
