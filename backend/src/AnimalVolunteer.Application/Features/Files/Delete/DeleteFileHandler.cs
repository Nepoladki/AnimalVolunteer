using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Application.Features.Files.Delete;

public class DeleteFileHandler
{
    private readonly IFileProvider _fileProvider;
    public DeleteFileHandler(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }
    public async Task<Result<bool, Error>> Delete(DeleteFileRequest request, CancellationToken cancellationToken)
    {
        return await _fileProvider.DeleteFile(request, cancellationToken);
    }
}
