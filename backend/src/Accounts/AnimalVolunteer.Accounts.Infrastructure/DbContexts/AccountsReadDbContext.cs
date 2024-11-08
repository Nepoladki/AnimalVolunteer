using AnimalVolunteer.Accounts.Application.Interfaces;
using AnimalVolunteer.Accounts.Domain.Models;
using AnimalVolunteer.Accounts.Domain.Models.AccountTypes;
using AnimalVolunteer.Core.DTOs.Accounts;
using AnimalVolunteer.Core.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace AnimalVolunteer.Accounts.Infrastructure.DbContexts;

public class AccountsReadDbContext : DbContext, IReadDbContext
{
    private readonly IConfiguration _configuration;
    private readonly DatabaseOptions _dbOptions;
    private readonly Assembly _assembly;

    public AccountsReadDbContext(
        IConfiguration configuration, IOptions<DatabaseOptions> dbOptions)
    {
        _configuration = configuration;
        _dbOptions = dbOptions.Value;
        _assembly = Assembly.GetExecutingAssembly();
    }

    public IQueryable<UserDto> Users => Set<UserDto>();
    public IQueryable<ParticipantAccountDto> Participants => Set<ParticipantAccountDto>();
    public IQueryable<VolunteerAccountDto> Volunteers => Set<VolunteerAccountDto>();
    public IQueryable<AdminAccountDto> Admins => Set<AdminAccountDto>();

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);

        builder.UseNpgsql(_configuration.GetConnectionString(_dbOptions.PostgresConnectionName));
        builder.UseSnakeCaseNamingConvention();
        builder.EnableSensitiveDataLogging();
        builder.UseLoggerFactory(new LoggerFactory());
        builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("accounts");

        builder.ApplyConfigurationsFromAssembly(
            _assembly,
            type => type.FullName?.Contains("Configurations.Read") ?? false);
    }
}
