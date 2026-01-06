namespace FormulaOnce.Commerce.Endpoints.Cart.AddToCart;

public record AddToCartRequest(Guid ProductId, int Quantity);