using System.Security.Claims;
using FastEndpoints;
using FormulaOnce.Commerce.Services.Cart;

namespace FormulaOnce.Commerce.Endpoints.Cart.RemoveFromCart;

public class RemoveFromCart : Endpoint<RemoveFromCartRequest>
{
    private readonly ICartService _cartService;

    public RemoveFromCart(ICartService cartService)
    {
        _cartService = cartService;
    }

    public override void Configure()
    {
        Post("/commerce/cart/remove");
    }

    public override async Task HandleAsync(RemoveFromCartRequest req, CancellationToken ct)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null)
        {
            await Send.NotFoundAsync(ct);
        }

        var removeResult = await _cartService.RemoveFromCartAsync(Guid.Parse(userId!), req.ProductId, ct);
        if (removeResult.IsSuccess)
        {
            await Send.NoContentAsync(ct);
            return;
        }

        await Send.NotFoundAsync(ct);
    }
}