namespace AnimalVolunteer.Volunteers.Infrastructure.Files;

public interface IFilesCleanerService
{
    Task Process(CancellationToken cancellationToken);
}