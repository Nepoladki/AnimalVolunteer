using AnimalVolunteer.Core.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace AnimalVolunteer.Core.Factories;

public class SqlConnectionFactory
{
    private readonly IConfiguration _configuration;
    private readonly DatabaseOptions _options;
    public SqlConnectionFactory(
        IConfiguration configuration, IOptions<DatabaseOptions> options)
    {
        _configuration = configuration;
        _options = options.Value;
    }

    public IDbConnection Create() =>
        new NpgsqlConnection(_configuration.GetConnectionString(_options.PostgresConnectionName));
}

