using AnimalVolunteer.Core.Options;
using AnimalVolunteer.VolunteerRequests.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace AnimalVolunteer.VolunteerRequests.Infrastructure.DbContexts;
public class WriteDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    private readonly DatabaseOptions _options;

    public WriteDbContext(IConfiguration configuration, 
        IOptions<DatabaseOptions> options)
    {
        _configuration = configuration;
        _options = options.Value;
    }
    public DbSet<VolunteerRequest> VolunteerRequests => Set<VolunteerRequest>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseNpgsql(_configuration
            .GetConnectionString(_options.PostgresConnectionName));
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("volunteer_requests");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
