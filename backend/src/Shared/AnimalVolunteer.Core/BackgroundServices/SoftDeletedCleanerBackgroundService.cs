using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AnimalVolunteer.Core.BackgroundServices;
public class SoftDeletedCleanerBackgroundService : BackgroundService
{
    private readonly ILogger<SoftDeletedCleanerBackgroundService> _logger;
    private readonly IServiceScopeFactory _scopeFactory;

    public SoftDeletedCleanerBackgroundService(
        ILogger<SoftDeletedCleanerBackgroundService> logger, 
        IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("SoftDeletedCleanerBackgoundService starts");

        await using var scope = _scopeFactory.CreateAsyncScope();
        var cleanerService = scope.ServiceProvider.GetRequiredService<ISoftDeletedCleaner>();
    }
}
