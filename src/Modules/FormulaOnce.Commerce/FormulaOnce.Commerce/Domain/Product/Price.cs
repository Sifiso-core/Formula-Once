using Ardalis.GuardClauses;

namespace FormulaOnce.Commerce.Domain.Product;

public record Price(decimal Amount, string Currency = "USD")
{
    public static Price Create(decimal amount, string currency = "USD")
    {
        Guard.Against.Negative(amount, nameof(amount));

        Guard.Against.NullOrEmpty(currency, nameof(currency));

        return new Price(amount, currency);
    }
}