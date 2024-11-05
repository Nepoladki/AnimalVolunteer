namespace AnimalVolunteer.Core.Options;

public class SoftDeletedCleanerOptions
{
    public const string SECTION_NAME = "BackgroundServices:SoftDeletedCleaner";
    public int CleaningIntervalHours { get; set; }
}

