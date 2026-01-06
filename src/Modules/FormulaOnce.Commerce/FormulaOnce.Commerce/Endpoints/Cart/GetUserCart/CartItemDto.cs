namespace FormulaOnce.Commerce.Endpoints.Cart.GetUserCart;

public record CartItemDto(
    Guid ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice,
    decimal SubTotal);