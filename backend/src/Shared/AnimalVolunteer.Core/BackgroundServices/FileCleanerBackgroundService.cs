using AnimalVolunteer.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AnimalVolunteer.Core.BackgroundServices;

public class FileCleanerBackgroundService : BackgroundService
{
    private readonly ILogger<FileCleanerBackgroundService> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    public FileCleanerBackgroundService(
        ILogger<FileCleanerBackgroundService> logger,
        IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("FileCeanerBackGroundService starts");

        await using var scope = _scopeFactory.CreateAsyncScope();
        var filesCleanerService = scope.ServiceProvider.GetRequiredService<IFilesCleanerService>();

        while (stoppingToken.IsCancellationRequested)
        {
            await filesCleanerService.Process(stoppingToken);
        }
        await Task.CompletedTask;
    }
}
