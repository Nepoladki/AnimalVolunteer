﻿using AnimalVolunteer.Accounts.Domain.Models;
using AnimalVolunteer.Accounts.Domain.Models.AccountTypes;
using AnimalVolunteer.Accounts.Domain.Models.ValueObjects;
using AnimalVolunteer.Core.DTOs.Accounts;
using AnimalVolunteer.Core.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Text.Json;

namespace AnimalVolunteer.Accounts.Infrastructure.DbContexts;
public class AccountsWriteDbContext : IdentityDbContext<User, Role, Guid>
{
    private readonly IConfiguration _config;
    private readonly DatabaseOptions _dbOptions;
    private readonly Assembly _assembly;

    public DbSet<RolePermission> RolesPermissions { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<ParticipantAccount> ParticipantsAccounts { get; set; }
    public DbSet<VolunteerAccount> VolunteerAccounts { get; set; }
    public DbSet<AdminAccount> AdminAccounts { get; set; }
    public DbSet<RefreshSession> RefreshSessions { get; set; }

    public AccountsWriteDbContext(
        IConfiguration configuration,
        IOptions<DatabaseOptions> dbOptions)
    {
        _dbOptions = dbOptions.Value;
        _config = configuration;
        _assembly = Assembly.GetExecutingAssembly();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseNpgsql(_config.GetConnectionString(_dbOptions.PostgresConnectionName));
        builder.UseSnakeCaseNamingConvention();
        builder.EnableSensitiveDataLogging();
        builder.UseLoggerFactory(new LoggerFactory());
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
            
        builder.HasDefaultSchema("accounts");

        builder.Entity<IdentityUserClaim<Guid>>().ToTable("user_claims");
        builder.Entity<IdentityUserToken<Guid>>().ToTable("user_tokens");
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("user_logins");
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("role_claims");
        builder.Entity<IdentityUserRole<Guid>>().ToTable("user_roles");

        builder.ApplyConfigurationsFromAssembly(
            _assembly, 
            type => type.FullName?.Contains("Configurations.Write") ?? false);
    }
}
