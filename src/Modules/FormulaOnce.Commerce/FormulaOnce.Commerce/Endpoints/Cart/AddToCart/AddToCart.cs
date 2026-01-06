using System.Security.Claims;
using FastEndpoints;
using FormulaOnce.Commerce.Services.Cart;
using Microsoft.AspNetCore.Http;

namespace FormulaOnce.Commerce.Endpoints.Cart.AddToCart;

internal class AddToCart(ICartService cartService) : Endpoint<AddToCartRequest>
{
    public override void Configure()
    {
        Post("/commerce/cart");
    }

    public override async Task HandleAsync(AddToCartRequest req, CancellationToken ct)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await cartService.AddToCartAsync(userId, req.ProductId, req.Quantity, ct);

        if (result.IsSuccess)
        {
            await Send.ResultAsync(TypedResults.Ok(result.Value));
        }
        else
        {
            await Send.ResultAsync(TypedResults.BadRequest(result.Errors));
        }
    }
}