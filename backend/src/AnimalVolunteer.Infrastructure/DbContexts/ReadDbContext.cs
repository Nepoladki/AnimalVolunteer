using AnimalVolunteer.Application.DTOs.Volunteer;
using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Aggregates.PetType;
using AnimalVolunteer.Domain.Aggregates.Volunteer.Root;
using AnimalVolunteer.Infrastructure.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AnimalVolunteer.Infrastructure.DbContexts;

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

    public IQueryable<VolunteerDto> Volunteers => Set<VolunteerDto>();
    public IQueryable<PetDto> Pets => Set<PetDto>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration
            .GetConnectionString(_dbOptions.PostgresConnectionName));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ReadDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Read") ?? false);
    }

    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => builder.AddConsole());
}
