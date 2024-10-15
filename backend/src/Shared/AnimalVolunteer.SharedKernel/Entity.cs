namespace AnimalVolunteer.SharedKernel;

public abstract class Entity<TId> where TId : notnull
{
    // EF Core ctor
    protected Entity(TId id) => Id = id;
    public TId Id { get; protected set; }
}
