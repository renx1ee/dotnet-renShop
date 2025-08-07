using Microsoft.EntityFrameworkCore;
using RenStore.Application.Common.Exceptions;
using RenStore.Application.Repository;
using RenStore.Domain.Entities;

namespace RenStore.Persistence.Repository;

public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly ApplicationDbContext context;
    public ShoppingCartRepository(ApplicationDbContext context) =>
        this.context = context;
    
    public async Task AddToCartAsync(
        ShoppingCartItem shoppingCartItem, 
        CancellationToken cancellationToken)
    {
        await context.ShoppingCartItems.AddAsync(shoppingCartItem, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task UpdateItemAsync(
        ShoppingCartItem shoppingCartItem, 
        CancellationToken cancellationToken)
    {
        context.ShoppingCartItems.Update(shoppingCartItem);
        await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task RemoveFromCartAsync(Guid itemId, 
        CancellationToken cancellationToken)
    {
        var item = await GetByItemIdAsync(itemId, cancellationToken)
            ?? throw new NotFoundException(typeof(ShoppingCartItem), itemId);
        
        context.ShoppingCartItems.Remove(item);
        await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task ClearFromCartAsync(Guid shoppingCartId, CancellationToken cancellationToken)
    {
    }
    
    public async Task<IList<ShoppingCartItem>> GetAllItemsAsync(
        CancellationToken cancellationToken)
    {
        return await context.ShoppingCartItems
                .ToListAsync(cancellationToken)
            ?? throw new NotFoundException(typeof(ShoppingCartItem), null);
    }

    public async Task<IList<ShoppingCartItem>> GetCartByUserIdAsync(string userId,
        CancellationToken cancellationToken)
    {
        return await context.ShoppingCartItems
            .Where(item => item.ApplicationUserId == userId)
                .ToListAsync(cancellationToken)
            ?? throw new NotFoundException(typeof(ShoppingCartItem), userId);
    }
    
    public async Task<ShoppingCartItem> GetItemAsync(string userId, Guid productId,
        CancellationToken cancellationToken)
    {
        return await context.ShoppingCartItems
            .FirstOrDefaultAsync(item => 
                item.ApplicationUserId == userId &&
                item.Product.Id == productId);
    }
    
    public async Task<ShoppingCartItem> GetByItemIdAsync(Guid itemId,
        CancellationToken cancellationToken)
    {
        return await context.ShoppingCartItems
            .FirstOrDefaultAsync(item => item.Id == itemId, cancellationToken)
            ?? throw new NotFoundException(typeof(ShoppingCartItem), itemId);
    }
    
    public async Task<ShoppingCartItem?> GetItemUserIdAndProductIdAsync(
        Guid productId,
        string userId,
        CancellationToken cancellationToken)
    {
        return await context.ShoppingCartItems
            .FirstOrDefaultAsync(item => 
                item.ProductId == productId &&
                item.ApplicationUserId == userId,
                cancellationToken);
    }
}