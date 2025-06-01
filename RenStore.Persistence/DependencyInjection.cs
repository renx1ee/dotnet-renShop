using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RenStore.Persistence.Entities.Category.Command.Create;
using RenStore.Persistence.Entities.Category.Command.Delete;
using RenStore.Persistence.Entities.Category.Command.Update;
using RenStore.Persistence.Entities.Category.Queries.GetAllCategories;
using RenStore.Persistence.Entities.Category.Queries.GetCategoryById;
using RenStore.Persistence.Entities.Category.Queries.GetCategoryByName;
using RenStore.Persistence.Entities.Orders.Commands.Create;
using RenStore.Persistence.Entities.Orders.Commands.Delete;
using RenStore.Persistence.Entities.Orders.Commands.Update;
using RenStore.Persistence.Entities.Orders.Queries.GetById;
using RenStore.Persistence.Entities.Orders.Queries.GetByProductId;
using RenStore.Persistence.Entities.Orders.Queries.GetByUserId;
using RenStore.Persistence.Entities.Product.Command.Create.Clothes;
using RenStore.Persistence.Entities.Product.Command.Create.Shoes;
using RenStore.Persistence.Entities.Product.Command.Update;
using RenStore.Persistence.Entities.Product.Queries.GetAllMinimizedProducts;
using RenStore.Persistence.Entities.Product.Queries.GetByNovelty;
using RenStore.Persistence.Entities.Product.Queries.GetBySellerId;
using RenStore.Persistence.Entities.Product.Queries.GetMyArticle;
using RenStore.Persistence.Entities.Product.Queries.GetProduct;
using RenStore.Persistence.Entities.Product.Queries.GetSortedByPrice;
using RenStore.Persistence.Entities.Product.Queries.GetSortedByRating;
using RenStore.Persistence.Entities.Review.Commands.Create;
using RenStore.Persistence.Entities.Review.Commands.Delete;
using RenStore.Persistence.Entities.Review.Commands.Update;
using RenStore.Persistence.Entities.Review.Queries.GetAllByProductId;
using RenStore.Persistence.Entities.Review.Queries.GetAllReviewsByUserId;
using RenStore.Persistence.Entities.Review.Queries.GetFirstByCreatedDate;
using RenStore.Persistence.Entities.Review.Queries.GetFirstByRating;
using RenStore.Persistence.Entities.Seller.Command.Create;
using RenStore.Persistence.Entities.Seller.Command.Delete;
using RenStore.Persistence.Entities.Seller.Command.Update;
using RenStore.Persistence.Entities.Seller.Queries.GetAll;
using RenStore.Persistence.Entities.Seller.Queries.GetByName;
using RenStore.Persistence.Entities.ShoppingCart.Command.Add;
using RenStore.Persistence.Entities.ShoppingCart.Command.Clear;
using RenStore.Persistence.Entities.ShoppingCart.Command.Remove;
using RenStore.Persistence.Entities.ShoppingCart.Query.GetAll;
using RenStore.Persistence.Entities.ShoppingCart.Query.GetByUserId;
using RenStore.Persistence.Entities.ShoppingCart.Query.GetTotalPrice;

namespace RenStore.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Product
        services.AddMediatR(x =>
            x.RegisterServicesFromAssemblies(
                typeof(CreateClothesProductCommandHandler).Assembly,
                typeof(CreateShoesProductCommandHandler).Assembly,
                typeof(UpdateProductCommandHandler).Assembly,
                typeof(GetProductByIdQueryHandler).Assembly,
                typeof(GetMiniProductListQueryHandler).Assembly,
                typeof(GetProductByArticleQueryHandler).Assembly,
                typeof(GetSortedProductsByPriceQueryHandler).Assembly,
                typeof(GetSortedProductsByRatingQueryHandler).Assembly,
                typeof(GetProductBySellerIdQueryHandler).Assembly,
                typeof(GetProductByNoveltyQueryHandler).Assembly
                ));
        // Category
        services.AddMediatR(x =>
            x.RegisterServicesFromAssemblies(
                typeof(CreateCategoryCommandHandler).Assembly,
                typeof(UpdateCategoryCommandHandler).Assembly,
                typeof(DeleteCategoryCommandHandler).Assembly,
                typeof(GetAllCategoriesQueryHandler).Assembly,
                typeof(GetCategoryByIdQueryHandler).Assembly,
                typeof(GetCategoryByNameQueryHandler).Assembly));
        // Seller
        services.AddMediatR(x =>
            x.RegisterServicesFromAssemblies(
                typeof(CreateSellerCommandHandler).Assembly,
                typeof(UpdateSellerCommandHandler).Assembly,
                typeof(DeleteSellerCommandHandler).Assembly,
                typeof(GetAllSellersQueryHandler).Assembly,
                typeof(GetCategoryByIdQueryHandler).Assembly,
                typeof(GetSellerByNameQueryHandler).Assembly));
        // Review
        services.AddMediatR(x =>
            x.RegisterServicesFromAssemblies(
                typeof(CreateReviewCommandHandler).Assembly,
                typeof(UpdateReviewCommandHandler).Assembly,
                typeof(DeleteReviewCommandHandler).Assembly,
                typeof(GetAllCategoriesQueryHandler).Assembly,
                typeof(GetAllReviewsByProductIdQueryHandler).Assembly,
                typeof(GetAllReviewsByUserIdQueryHandler).Assembly));
        // Order
        services.AddMediatR(x =>
            x.RegisterServicesFromAssemblies(
                typeof(CreateOrderCommandHandler).Assembly,
                typeof(UpdateOrderCommandHandler).Assembly,
                typeof(DeleteOrderCommandHandler).Assembly,
                typeof(GetAllCategoriesQueryHandler).Assembly,
                typeof(GetOrderByIdQueryHandler).Assembly,
                typeof(GetOrdersByProductIdQueryHandler).Assembly,
                typeof(GetOrdersByUserIdQueryHandler).Assembly,
                typeof(GetFirstReviewsByRatingQueryHandler).Assembly,
                typeof(GetFirstReviewsByCreatedDateQueryHandler).Assembly));
        // Shopping Cart
        services.AddMediatR(x =>
            x.RegisterServicesFromAssemblies(
                typeof(AddToCartCommandHandler).Assembly,
                typeof(RemoveFromCartCommandHandler).Assembly,
                typeof(ClearCartCommandHandler).Assembly,
                typeof(GetShoppingCartItemsByUserIdQueryHandler).Assembly,
                typeof(GetTotalShoppingCartPriceQueryHandler).Assembly,
                typeof(GetAllCartItemsQueryHandler).Assembly,
                typeof(GetTotalShoppingCartPriceQueryHandler).Assembly
                ));
        
        return services;
    }
}