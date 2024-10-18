using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Volunteers.Domain.ValueObjects.Pet;

public record Address
{
    public const int MAX_LENGTH = 50;
    public const string DB_COLUMN_COUNTRY = "country";
    public const string DB_COLUMN_CITY = "city";
    public const string DB_COLUMN_STREET = "street";
    public const string DB_COLUMN_HOUSE = "house";
    private Address(string country, string city, string street, string? house)
    {
        Country = country;
        City = city;
        Street = street;
        House = house;
    }
    public string Country { get; }
    public string City { get; }
    public string Street { get; }
    public string? House { get; }
    public static Result<Address, Error> Create(string country, string city, string street, string? house)
    {
        if (string.IsNullOrWhiteSpace(country) || country.Length > MAX_LENGTH)
            return Errors.General.InvalidValue(nameof(country));

        if (string.IsNullOrWhiteSpace(city) || city.Length > MAX_LENGTH)
            return Errors.General.InvalidValue(nameof(city));

        if (string.IsNullOrWhiteSpace(street) || street.Length > MAX_LENGTH)
            return Errors.General.InvalidValue(nameof(street));

        if (house is not null && house.Length > MAX_LENGTH)
            return Errors.General.InvalidValue(nameof(house));

        return new Address(country, city, street, house);
    }
}
