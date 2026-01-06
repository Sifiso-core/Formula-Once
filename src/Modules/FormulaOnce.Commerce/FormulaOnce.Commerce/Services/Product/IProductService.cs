using Ardalis.Result;
using FormulaOnce.Commerce.Endpoints.Product.CreateProduct;

namespace FormulaOnce.Commerce.Services.Product;

public interface IProductService
{
    Task<Result<Guid>> CreateProductAsync(CreateProductRequest request, CancellationToken ct);
}