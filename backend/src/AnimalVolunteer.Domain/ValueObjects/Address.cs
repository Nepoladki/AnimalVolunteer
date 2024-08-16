﻿using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Domain.ValueObjects;

public record Address
{
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
    public static Result<Address> Create(string country, string city, string street, string house)
    {
        if (string.IsNullOrWhiteSpace(country) || country.Length > Constants.TEXT_LENGTH_LIMIT_LOW)
            return Result.Failure<Address>("Invalid country");

        if (string.IsNullOrWhiteSpace(city) || city.Length > Constants.TEXT_LENGTH_LIMIT_LOW)
            return Result.Failure<Address>("Invalid city");

        if (string.IsNullOrWhiteSpace(street) || street.Length > Constants.TEXT_LENGTH_LIMIT_LOW)
            return Result.Failure<Address>("Invalid street");

        if (house is not null && house.Length > Constants.TEXT_LENGTH_LIMIT_LOW)
            return Result.Failure<Address>("Invalid house");

        return Result.Success(new Address(country, city, street, house));
    }
}