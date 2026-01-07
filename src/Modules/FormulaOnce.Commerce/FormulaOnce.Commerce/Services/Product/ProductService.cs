using Ardalis.Result;
using FormulaOnce.Commerce.Domain;
using FormulaOnce.Commerce.Domain.Product;
using FormulaOnce.Commerce.Endpoints.Product.CreateProduct;
using FormulaOnce.Commerce.Infrastructure.Repositories;
using FormulaOnce.Commerce.Infrastructure.Repositories.Product;

namespace FormulaOnce.Commerce.Services.Product;

public class ProductService(IProductRepository productRepository) : IProductService
{
    public async Task<Result<Guid>> CreateProductAsync(CreateProductRequest request, CancellationToken ct)
    {
        var price = Price.Create(request.Price, request.Currency);

        var productResult = Domain.Product.Product.Create(
            request.Name, request.Description,
            price,
            request.Stock);

        if (!productResult.IsSuccess)
        {
            return productResult.Map(x => x.Id);
        }

        await productRepository.AddAsync(productResult.Value, ct);

        return Result.Success(productResult.Value.Id);
    }
}