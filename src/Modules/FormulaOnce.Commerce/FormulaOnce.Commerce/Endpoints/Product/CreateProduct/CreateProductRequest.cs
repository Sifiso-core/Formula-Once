using FormulaOnce.Commerce.Domain.Product;

namespace FormulaOnce.Commerce.Endpoints.Product.CreateProduct;

public record CreateProductRequest(
    string Name,
    string Description,
    decimal Price,
    string Currency,
    int Stock,
    ProductType Type);