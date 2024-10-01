using AnimalVolunteer.Application.DTOs.Volunteer.Pet;
using AnimalVolunteer.Application.Interfaces;

namespace AnimalVolunteer.Infrastructure.Files;

public class FilesCleanerService : IFilesCleanerService
{
    private readonly IMessageQueue<IEnumerable<FileInfoDto>> _messageQueue;
    private readonly IFileProvider _fileProvider;

    public FilesCleanerService(
        IMessageQueue<IEnumerable<FileInfoDto>> messageQueue,
        IFileProvider fileProvider)
    {
        _messageQueue = messageQueue;
        _fileProvider = fileProvider;
    }

    public async Task Process(CancellationToken cancellationToken)
    {
        var fileInfos = await _messageQueue.ReadAsync(cancellationToken);

        foreach (var fileInfo in fileInfos)
        {
            await _fileProvider.DeleteFile(fileInfo, cancellationToken);
        }
    }
}
