using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Domain.Aggregates.VolunteerManagement.ValueObjects.Pet;

public class Position : ValueObject
{
    public int Value { get; }
    private Position(int value)
    {
        Value = value;
    }
    public static Result<Position, Error> Create(int number)
    {
        if (number < 1)
            return Errors.General.InvalidValue(nameof(Position));

        return new Position(number);
    }

    public Result<Position, Error> Forward() => Create(Value + 1);
    public Result<Position, Error> Backward() => Create(Value - 1);

    public static implicit operator int(Position pos) => pos.Value;

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}
