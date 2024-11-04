namespace AnimalVolunteer.Core.BackgroundServices;

public interface ISoftDeletedCleaner
{
    void Process(CancellationToken cancellationToken);
}