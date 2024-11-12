using AnimalVolunteer.Core.Options;
using AnimalVolunteer.Discussions.Domain.Aggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace AnimalVolunteer.Discussions.Infrastructure.DbContexts;

public class WriteDbContext : DbContext
{
    private readonly DatabaseOptions _options;
    private readonly IConfiguration _configuration;

    public WriteDbContext(
        IConfiguration configuration, IOptions<DatabaseOptions> options)
    {
        _configuration = configuration;
        _options = options.Value;
    }

    public DbSet<Discussion> Discussions => Set<Discussion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseNpgsql(_configuration
            .GetConnectionString(_options.PostgresConnectionName));
        optionsBuilder.UseLowerCaseNamingConvention();
        optionsBuilder.EnableSensitiveDataLogging();
    }
}

