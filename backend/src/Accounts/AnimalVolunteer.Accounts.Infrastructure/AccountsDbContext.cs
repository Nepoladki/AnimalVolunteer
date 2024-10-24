using AnimalVolunteer.Accounts.Domain.Models;
using AnimalVolunteer.Accounts.Domain.Models.Users;
using AnimalVolunteer.Core.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace AnimalVolunteer.Accounts.Infrastructure;
public class AccountsDbContext : IdentityDbContext<User, Role, Guid>
{
    private readonly IConfiguration _config;
    private readonly DatabaseOptions _dbOptions;

    public DbSet<RolePermission> RolesPermissions { get; set; }
    public DbSet<Permission> Permissions { get; set; }

    public AccountsDbContext(
        IConfiguration configuration, 
        IOptions<DatabaseOptions> dbOptions)
    {
        _dbOptions = dbOptions.Value;
        _config = configuration;
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

        builder.Entity<User>().ToTable("users");
        builder.Entity<User>().Property(u => u.SocialNetworks)
            .HasConversion(
            sn => JsonSerializer.Serialize(sn, JsonSerializerOptions.Default), 
            json => JsonSerializer.Deserialize<List<SocialNetwork>>(json, JsonSerializerOptions.Default)!);


        builder.Entity<Role>().ToTable("roles");
        builder.Entity<Permission>().ToTable("permissions").HasIndex(p => p.CodeName).IsUnique();
        builder.Entity<IdentityUserClaim<Guid>>().ToTable("user_claims");
        builder.Entity<IdentityUserToken<Guid>>().ToTable("user_tokens");
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("user_logins");
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("role_claims");
        builder.Entity<IdentityUserRole<Guid>>().ToTable("user_roles");

        builder.Entity<RolePermission>().ToTable("roles_permissions")
            .HasKey(rp => new {rp.RoleId, rp.PermissionId});

        builder.Entity<RolePermission>()
            .HasOne(rp => rp.Role)
            .WithMany(r => r.RolePermissions)
            .HasForeignKey(rp => rp.RoleId);

        builder.Entity<RolePermission>()
            .HasOne(rp => rp.Permission)
            .WithMany()
            .HasForeignKey(rp => rp.PermissionId);
    }
}  
