using AnimalVolunteer.Application.Database;
using AnimalVolunteer.Application.Features.Volunteer.AddPet;
using AnimalVolunteer.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace AnimalVolunteer.Application.Features.Volunteer.AddPetPhotos;

public class AddPetPhotosHandler
{
    private const string BUCKET_NAME = "photos";
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IFileProvider _fileProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddPetHandler> _logger;

    public AddPetPhotosHandler(
        IVolunteerRepository volunteerRepository,
        IFileProvider fileProvider,
        IUnitOfWork unitOfWork,
        ILogger<AddPetHandler> logger)
    {
        _volunteerRepository = volunteerRepository;
        _fileProvider = fileProvider;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

}
