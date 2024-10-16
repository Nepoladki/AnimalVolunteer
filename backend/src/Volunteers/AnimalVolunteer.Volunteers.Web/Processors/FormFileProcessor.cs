using AnimalVolunteer.Core.DTOs.Volunteers.Pet;
using Microsoft.AspNetCore.Http;

namespace AnimalVolunteer.Volunteers.Web.Processors;

public class FormFileProcessor : IAsyncDisposable
{
    private readonly List<UploadFileDto> _fileList = [];
    public List<UploadFileDto> Process(IFormFileCollection files)
    {
        foreach (var file in files)
        {
            var stream = file.OpenReadStream();
            var fileDto = new UploadFileDto(file.FileName, stream);
            _fileList.Add(fileDto);
        }

        return _fileList;
    }
    public async ValueTask DisposeAsync()
    {
        foreach (var file in _fileList)
        {
            await file.Content.DisposeAsync();
        }
    }
}
