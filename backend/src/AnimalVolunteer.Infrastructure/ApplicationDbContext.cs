using AnimalVolunteer.Domain.Aggregates.PetType;
using AnimalVolunteer.Domain.Aggregates.Volunteer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AnimalVolunteer.Infrastructure;

public class ApplicationDbContext(IConfiguration configuration) : DbContext
{
    private const string CONNECTION_NAME = "Postgres";
    public DbSet<Volunteer> Volunteers { get; set; }
    public DbSet<Species> Species { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(CONNECTION_NAME));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => builder.AddConsole());
}
