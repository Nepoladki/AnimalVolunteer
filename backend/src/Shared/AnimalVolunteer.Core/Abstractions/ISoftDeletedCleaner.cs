namespace AnimalVolunteer.Core.Abstractions;

public interface ISoftDeletedCleaner
{
    Task Process(CancellationToken cancellationToken);
}