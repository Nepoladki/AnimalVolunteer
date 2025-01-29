using AnimalVolunteer.Core;
using AnimalVolunteer.Core.Abstractions;
using AnimalVolunteer.Core.Factories;
using AnimalVolunteer.Volunteers.Infrastructure.DbContexts;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace AnimalVolunteer.Volunteers.Infrastructure.Services;
public class SoftDeletedVolunteersCleanerService : ISoftDeletedCleaner
{
    private readonly SqlConnectionFactory _sqlConnectionFactory;

    public SoftDeletedVolunteersCleanerService(
        SqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task Process(CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.Create();

        var petsCommand =
            """
            DELETE FROM public.pets p 
            WHERE 
            p.is_deleted = TRUE 
            AND 
            now() - deletion_datetime > interval '30 days'
            """;

        var volunteersCommand =
             """
            DELETE FROM public.volunteers v 
            WHERE 
            v.is_deleted = TRUE 
            AND 
            now() - deletion_datetime > interval '30 days'
            """;

        connection.Open();

        await connection.ExecuteAsync(petsCommand);
        await connection.ExecuteAsync(volunteersCommand);

        connection.Close();
    }
}
