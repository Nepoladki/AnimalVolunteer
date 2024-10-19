namespace AnimalVolunteer.Core.Abstractions;

public interface IFilesCleanerService
{
    Task Process(CancellationToken cancellationToken);
}