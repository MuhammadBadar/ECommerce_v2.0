using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects;

public sealed record Address
{
    public string AddressLine { get; }
    public string City { get; }
    public string PostalCode { get; }
    public string Country { get; }

    private Address(
        string addressLine,
        string city,
        string postalCode,
        string country)
    {
        if (string.IsNullOrWhiteSpace(addressLine))
            throw new ArgumentException("Address is required.");

        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("City is required.");

        if (string.IsNullOrWhiteSpace(country))
            throw new ArgumentException("Country is required.");

        AddressLine = addressLine;
        City = city;
        PostalCode = postalCode;
        Country = country;
    }

    public static Address Create(
        string addressLine,
        string city,
        string postalCode,
        string country)
    {
        return new Address(
            addressLine,
            city,
            postalCode,
            country);
    }
}
