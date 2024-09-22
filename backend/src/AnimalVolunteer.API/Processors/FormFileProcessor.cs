
using AnimalVolunteer.Application.DTOs.Volunteer.Pet;

namespace AnimalVolunteer.API.Processors;

public class FormFileProcessor : IAsyncDisposable
{
    private readonly List<PetPhotoDto> _petPhotoDtos = [];
    public List<PetPhotoDto> Process(IFormFileCollection files)
    {
        foreach (var file in files)
        {
            var stream = file.OpenReadStream();
            var fileDto = new PetPhotoDto(file.FileName, stream, false); // HARDCODED FALSE 
            _petPhotoDtos.Add(fileDto);
        }

        return _petPhotoDtos;
    }
    public async ValueTask DisposeAsync()
    {
        foreach (var file in _petPhotoDtos)
        {
            await file.Content.DisposeAsync();
        }
    }
}
