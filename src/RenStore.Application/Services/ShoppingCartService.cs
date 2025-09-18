﻿using Microsoft.Extensions.Logging;
using RenStore.Application.Repository;
using RenStore.Domain.Entities;

namespace RenStore.Application.Services;

public class ShoppingCartService 
{
    private readonly ILogger<ShoppingCartService> logger;
    private readonly IShoppingCartRepository shoppingCartRepository;

    public ShoppingCartService(
        ILogger<ShoppingCartService> logger,
        IShoppingCartRepository shoppingCartRepository)
    {
        this.logger = logger;
        this.shoppingCartRepository = shoppingCartRepository;
    }

    // TODO: исправить баг
    public async Task<decimal> GetShoppingCartTotal(IList<ShoppingCartItem> shoppingCartItems)
    {
        logger.LogInformation("Get shopping cart method is starting.");

        try
        {
            var result = shoppingCartItems.Select(cart => 
                cart.Product.Price * cart.Amount)
                .Sum();
            
            logger.LogInformation("Get shopping cart method is stopped.");

            return result;
        } 
        catch (Exception ex)
        {
            logger.LogInformation($"Get shopping cart method error. Error: {ex.Message}");
            throw;
        }
    }
    
    /*

    public Guid ShoppingCartId { get; set; }

    public IEnumerable<ShoppingCartItem> ShoppingCartItems { get; set; }
    
    /*public static ShoppingCartService GetCart(IServiceProvider services)
    {
        ISession session = services.GetRequiredService<IHttpContextAccessor>()?
            .HttpContext.Session;
        
        var context = services.GetService<IApplicationDbContext>();
        string cartId = session.GetString("CartId")
        if (cartId is null || cartId == Guid.Empty.ToString())
        {
            CreateCartAsync();
        }

        session.SetString("CartId", cartId);
        
        return new ShoppingCartService(context)
        {
            ShoppingCartId = Guid.Parse(cartId)
        };
    }#1#

    public async Task<ShoppingCart> GetCartAsync(Guid shoppingCartId)
    {
       return await shoppingCartRepository.GetCartAsync(shoppingCartId, CancellationToken.None);
    }

    public async Task CreateCartAsync(string userId)
    {
        var cart = await shoppingCartRepository.GetCartByUserIdAsync(userId, CancellationToken.None);

        if (cart is null)
            await shoppingCartRepository
                .CreateShoppingCartAsync(
                    new ShoppingCart
                    {
                        UserId = userId,
                    }, 
                    CancellationToken.None);
    }
    
    /*public async Task<ShoppingCart> GetOrCreateCartAsync(string cartId)
    {
        var cart = await _context.ShoppingCarts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.CartId == cartId);

        if (cart == null)
        {
            cart = new ShoppingCart { CartId = cartId };
            _context.ShoppingCarts.Add(cart);
            await _context.SaveChangesAsync();
        }

        return cart;
    }

    */
}
