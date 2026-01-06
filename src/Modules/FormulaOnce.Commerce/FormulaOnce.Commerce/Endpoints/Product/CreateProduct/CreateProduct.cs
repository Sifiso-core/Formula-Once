using FastEndpoints;
using FormulaOnce.Commerce.Services.Cart;
using FormulaOnce.Commerce.Services.Product;
using Microsoft.AspNetCore.Http;

namespace FormulaOnce.Commerce.Endpoints.Product.CreateProduct;

internal class CreateProduct(ICartService cartService, IProductService productService)
    : Endpoint<CreateProductRequest>
{
    public override void Configure()
    {
        Post("/commerce/products");
        Policies("AdminOnly");
    }

    public override async Task HandleAsync(CreateProductRequest req, CancellationToken ct)
    {
        var result = await productService.CreateProductAsync(req, ct);

        if (result.IsSuccess)
        {
            await Send.OkAsync(result.Value, cancellation: ct);
            return;
        }

        await Send.ResultAsync(TypedResults.BadRequest(result.Errors));
    }
}