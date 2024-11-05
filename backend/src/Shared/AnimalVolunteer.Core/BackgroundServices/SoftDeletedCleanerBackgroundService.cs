using AnimalVolunteer.Core.Abstractions;
using AnimalVolunteer.Core.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AnimalVolunteer.Core.BackgroundServices;
public class SoftDeletedCleanerBackgroundService : BackgroundService
{
    private readonly ILogger<SoftDeletedCleanerBackgroundService> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly SoftDeletedCleanerOptions _options;

    public SoftDeletedCleanerBackgroundService(
        ILogger<SoftDeletedCleanerBackgroundService> logger,
        IServiceScopeFactory scopeFactory,
        IOptions<SoftDeletedCleanerOptions> options)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
        _options = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("SoftDeletedCleanerBackgoundService starts");

        await using var scope = _scopeFactory.CreateAsyncScope();
        var cleanerService = scope.ServiceProvider.GetRequiredService<ISoftDeletedCleaner>();

        while (true)
        {
            await cleanerService.Process(stoppingToken);
            await Task.Delay(TimeSpan.FromHours(_options.CleaningIntervalHours));
        }
    }
}
