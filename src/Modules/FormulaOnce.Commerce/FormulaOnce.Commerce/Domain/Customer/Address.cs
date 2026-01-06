using Ardalis.GuardClauses;

namespace FormulaOnce.Commerce.Domain.Customer;

public record Address(string Street, string City, string Province, string PostalCode, string Country)
{
    public static Address Create(string street, string city, string state, string zip, string country)
    {
        Guard.Against.NullOrWhiteSpace(street);
        Guard.Against.NullOrWhiteSpace(city);
        Guard.Against.NullOrWhiteSpace(country);
        Guard.Against.NullOrWhiteSpace(zip);
        Guard.Against.NullOrWhiteSpace(state);
        return new Address(street, city, state, zip, country);
    }
}