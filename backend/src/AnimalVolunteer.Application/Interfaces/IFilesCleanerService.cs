namespace AnimalVolunteer.Application.Interfaces;

public interface IFilesCleanerService
{
    Task Process(CancellationToken cancellationToken);
}