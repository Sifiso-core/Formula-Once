using System.ComponentModel.DataAnnotations;
using Ardalis.Result;

namespace FormulaOnce.Commerce.Domain.Cart;

public class Cart
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; } // The Owner

    // Encapsulation: The outside world cannot touch this list
    private readonly List<CartItem> _items = new();
    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();

    private Cart()
    {
    }

    public static Cart Create(Guid userId) => new() { Id = Guid.NewGuid(), UserId = userId };

    public Result AddOrUpdateItem(Guid productId, int quantity)
    {
        if (quantity <= 0)
            return Result.Invalid(new ValidationError("Quantity must be greater than zero"));

        var existing = _items.FirstOrDefault(x => x.ProductId == productId);
        if (existing != null)
        {
            existing.UpdateQuantity(existing.Quantity + quantity);
        }
        else
        {
            var cartItem = CartItem.Create(productId, Id, quantity);
            _items.Add(cartItem);
        }

        return Result.Success();
    }

    public void RemoveItem(Guid productId)
    {
        if (_items.Any(x => x.ProductId == productId))
        {
            _items.RemoveAll(x => x.ProductId == productId);
        }
    }

    public void Clear() => _items.Clear();
}