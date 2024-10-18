using AnimalVolunteer.Core.Options;
using AnimalVolunteer.Volunteers.Domain.Root;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AnimalVolunteer.Volunteers.Infrastructure.DbContexts;

public class WriteDbContext : DbContext
{
    private readonly DatabaseOptions _dbOptions;
    private readonly IConfiguration _configuration;
    private readonly ILoggerFactory _loggerFactory = new LoggerFactory();

    public WriteDbContext(
        IConfiguration configuration, IOptions<DatabaseOptions> dbOptions)
    {
        _configuration = configuration;
        _dbOptions = dbOptions.Value;
    }

    public DbSet<Volunteer> Volunteers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration
            .GetConnectionString(_dbOptions.PostgresConnectionName));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(_loggerFactory);
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(WriteDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Write") ?? false);
    }
}
