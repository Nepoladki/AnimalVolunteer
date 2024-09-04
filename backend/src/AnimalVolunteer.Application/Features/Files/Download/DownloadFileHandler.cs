using AnimalVolunteer.Application.Features.Files.GetUrl;
using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Application.Features.Files.Download;

public class DownloadFileHandler
{
    private readonly IFileProvider _fileProvider;
    public DownloadFileHandler(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }

    public async Task<Result<string, Error>> Download(
        DownloadFileRequest request, CancellationToken cancellationToken)
    {
        return await _fileProvider.GetFileUrl(request, cancellationToken);
    }
}
