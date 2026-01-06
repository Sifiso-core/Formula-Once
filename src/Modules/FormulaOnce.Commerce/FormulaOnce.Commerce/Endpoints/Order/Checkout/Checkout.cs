using System.Security.Claims;
using FastEndpoints;
using FormulaOnce.Commerce.Services.Order;
using Microsoft.AspNetCore.Http;

namespace FormulaOnce.Commerce.Endpoints.Order.Checkout;

internal class Checkout(IOrderService orderService) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post("/commerce/cart/checkout");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await orderService.CheckoutAsync(userId, ct);

        if (result.IsSuccess)
        {
            await Send.OkAsync(new { OrderId = result.Value }, cancellation: ct);
            return;
        }

        await Send.ResultAsync(TypedResults.BadRequest(result.Errors));
    }
}