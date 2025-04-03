using Amazon.S3;
using FileServiceAPI.Interfaces;
using Hangfire;

namespace FileServiceAPI.Infrastructure.Jobs;

public class ConfirmConsistencyJob
{
    private readonly IFilesRepository _fileRepository;
    private readonly IAmazonS3 _amazonS3;
    private readonly ILogger<ConfirmConsistencyJob> _logger;

    public ConfirmConsistencyJob(
        IFilesRepository fileRepository, 
        IAmazonS3 amazonS3, 
        ILogger<ConfirmConsistencyJob> logger)
    {
        _fileRepository = fileRepository;
        _amazonS3 = amazonS3;
        _logger = logger;
    }

    [AutomaticRetry(Attempts = 3, DelaysInSeconds = [5, 10, 15])]
    public void Execute(Guid fileId, string key)
    {
        _logger.LogInformation("Started ConfirmConsistencyJob with {fileId} and {key}", fileId, key);


    }
}
