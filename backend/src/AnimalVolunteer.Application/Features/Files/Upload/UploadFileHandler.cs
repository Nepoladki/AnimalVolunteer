using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Application.Features.Files.Upload;
public class UploadFileHandler
{
    private readonly IFileProvider _fileProvider;
    public UploadFileHandler(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }
    public async Task<Result<string, Error>> Upload(UploadFileRequest request, CancellationToken cancellationToken)
    {
        return await _fileProvider.UploadFile(request.FileData, cancellationToken);
    }
}
