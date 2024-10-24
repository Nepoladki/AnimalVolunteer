using AnimalVolunteer.Core.DTOs.Species;
using AnimalVolunteer.Core.Options;
using AnimalVolunteer.Species.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AnimalVolunteer.Species.Infrastructure.DbContexts;

public class ReadDbContext : DbContext, IReadDbContext
{
    private readonly DatabaseOptions _dbOptions;
    private readonly IConfiguration _configuration;

    public ReadDbContext(
        IConfiguration configuration, IOptions<DatabaseOptions> dbOptions)
    {
        _configuration = configuration;
        _dbOptions = dbOptions.Value;
    }
    public IQueryable<SpeciesDto> Species => Set<SpeciesDto>();
    public IQueryable<BreedDto> Breeds => Set<BreedDto>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration
            .GetConnectionString(_dbOptions.PostgresConnectionName));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(new LoggerFactory());
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ReadDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Read") ?? false);
    }
}
