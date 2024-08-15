﻿namespace AnimalVolunteer.Domain.Entities;

public class PetPhoto
{
    public Guid PetPhotoId { get; private set; }
    public string FilePath { get; private set; } = null!;
    public bool IsMain { get; private set; }
}
