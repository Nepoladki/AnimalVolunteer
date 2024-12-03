using AnimalVolunteer.Core.DTOs.VolunteerRequests;
using AnimalVolunteer.Core.Options;
using AnimalVolunteer.VolunteerRequests.Application.Interfaces;
using AnimalVolunteer.VolunteerRequests.Domain;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Mapping;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AnimalVolunteer.VolunteerRequests.Infrastructure.Linq2db;

public class Linq2DbConnection : DataConnection, ILinq2dbConnection
{

    public Linq2DbConnection(DataOptions<Linq2DbConnection> options)
        : base(options.Options)
    {

    }

    public ITable<VolunteerRequestDto> VolunteerRequests => 
        this.GetTable<VolunteerRequestDto>();
}
