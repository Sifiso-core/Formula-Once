namespace FormulaOnce.Commerce.Endpoints.Cart.GetUserCart;

public record CartDto(
    Guid UserId,
    List<CartItemDto> Items,
    decimal GrandTotal);