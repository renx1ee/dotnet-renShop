using RenStore.Domain.Entities;

namespace RenStore.Application.Repository;

public interface IShoppingCartRepository
{
    // TODO: доделать коментарии.
    Task AddToCartAsync(ShoppingCartItem shoppingCartItem, CancellationToken cancellationToken);
    Task UpdateItemAsync(ShoppingCartItem shoppingCartItem, CancellationToken cancellationToken);
    Task RemoveFromCartAsync(Guid itemId, CancellationToken cancellationToken);
    Task ClearFromCartAsync(Guid shoppingCartId, CancellationToken cancellationToken);
    Task<IList<ShoppingCartItem>> GetAllItemsAsync(CancellationToken cancellation);
    Task<ShoppingCartItem> GetItemAsync(string userId, Guid productId, CancellationToken cancellationToken);
    Task<IList<ShoppingCartItem>> GetCartByUserIdAsync(string userId, CancellationToken cancellationToken);
    Task<ShoppingCartItem> GetByItemIdAsync(Guid itemId, CancellationToken cancellationToken);
    Task<ShoppingCartItem?> GetItemUserIdAndProductIdAsync(Guid productId, string userId, CancellationToken cancellationToken);
}