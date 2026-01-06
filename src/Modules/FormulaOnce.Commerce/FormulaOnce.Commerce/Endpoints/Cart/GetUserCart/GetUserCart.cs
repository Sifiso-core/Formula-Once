using System.Security.Claims;
using FastEndpoints;
using FormulaOnce.Commerce.Infrastructure.Repositories.Cart;

namespace FormulaOnce.Commerce.Endpoints.Cart.GetUserCart;

internal class GetUserCart(ICartRepository cartRepository) : EndpointWithoutRequest<CartDto>
{
    public override void Configure()
    {
        Get("/commerce/cart");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var cartDto = await cartRepository.GetCartDtoByUserIdAsync(userId, ct);

        await Send.OkAsync(cartDto ?? new CartDto(userId, [], 0), cancellation: ct);
    }
}