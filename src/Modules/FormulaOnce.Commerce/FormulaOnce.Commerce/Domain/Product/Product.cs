using Ardalis.Result;

namespace FormulaOnce.Commerce.Domain.Product;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Price Price { get; private set; }
    public int StockQuantity { get; private set; }
    public ProductType Type { get; private set; }

    private Product()
    {
        // For EF
    }

    public static Result<Product> Create(string name, string description, Price price, int stock)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Invalid(new ValidationError("Name is required"));

        return new Product
        {
            Id = Guid.NewGuid(),
            Name = name,
            Price = price,
            Description = description,
            StockQuantity = stock
        };
    }

    public Result UpdateStock(int quantity)
    {
        if (StockQuantity + quantity < 0)
            return Result.Conflict("Insufficient stock for this operation.");

        StockQuantity += quantity;
        return Result.Success();
    }
}