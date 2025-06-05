using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using RenStore.Application.Common.Mappings.Category;
using RenStore.Application.Common.Mappings.Order;
using RenStore.Application.Common.Mappings.Product;
using RenStore.Application.Common.Mappings.Review;
using RenStore.Application.Common.Mappings.Seller;
using RenStore.Application.Common.Mappings.ShoppingCart;
using RenStore.Application.Entities.Category.Commands.Create;
using RenStore.Application.Entities.Category.Commands.Delete;
using RenStore.Application.Entities.Category.Commands.Update;
using RenStore.Application.Entities.Category.Queries.GetAllCategories;
using RenStore.Application.Entities.Category.Queries.GetCategoryById;
using RenStore.Application.Entities.Category.Queries.GetCategoryByName;
using RenStore.Application.Entities.Orders.Commands.Create;
using RenStore.Application.Entities.Orders.Commands.Delete;
using RenStore.Application.Entities.Orders.Commands.Update;
using RenStore.Application.Entities.Orders.Queries.GetAll;
using RenStore.Application.Entities.Orders.Queries.GetById;
using RenStore.Application.Entities.Orders.Queries.GetByProductId;
using RenStore.Application.Entities.Orders.Queries.GetByUserId;
using RenStore.Application.Entities.Product.Commands.Create.Clothes;
using RenStore.Application.Entities.Product.Commands.Create.Shoes;
using RenStore.Application.Entities.Product.Commands.Delete;
using RenStore.Application.Entities.Product.Commands.Update;
using RenStore.Application.Entities.Product.Queries.GetByArticle;
using RenStore.Application.Entities.Product.Queries.GetByName;
using RenStore.Application.Entities.Product.Queries.GetByNovelty;
using RenStore.Application.Entities.Product.Queries.GetBySellerId;
using RenStore.Application.Entities.Product.Queries.GetMinimizedProducts;
using RenStore.Application.Entities.Product.Queries.GetProductDetails;
using RenStore.Application.Entities.Product.Queries.GetSortedByPrice;
using RenStore.Application.Entities.Product.Queries.GetSortedByRating;
using RenStore.Application.Entities.Review.Commands.Create;
using RenStore.Application.Entities.Review.Commands.Delete;
using RenStore.Application.Entities.Review.Commands.Moderate;
using RenStore.Application.Entities.Review.Commands.Update;
using RenStore.Application.Entities.Review.Queries.GetAllByProductId;
using RenStore.Application.Entities.Review.Queries.GetAllForModeration;
using RenStore.Application.Entities.Review.Queries.GetAllReviews;
using RenStore.Application.Entities.Review.Queries.GetAllReviewsByUserId;
using RenStore.Application.Entities.Review.Queries.GetFirstByCreatedDate;
using RenStore.Application.Entities.Review.Queries.GetFirstByRating;
using RenStore.Application.Entities.Seller.Command.Create;
using RenStore.Application.Entities.Seller.Command.Delete;
using RenStore.Application.Entities.Seller.Command.Update;
using RenStore.Application.Entities.Seller.Queries.GetAll;
using RenStore.Application.Entities.Seller.Queries.GetById;
using RenStore.Application.Entities.Seller.Queries.GetByName;
using RenStore.Application.Entities.ShoppingCart.Command.Add;
using RenStore.Application.Entities.ShoppingCart.Command.Clear;
using RenStore.Application.Entities.ShoppingCart.Command.Remove;
using RenStore.Application.Entities.ShoppingCart.Query.GetAll;
using RenStore.Application.Entities.ShoppingCart.Query.GetByUserId;
using RenStore.Application.Entities.ShoppingCart.Query.GetTotalPrice;

namespace RenStore.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        // Product
        services.AddAutoMapper(typeof(CreateClothesProductMappingProfile));
        services.AddAutoMapper(typeof(CreateShoesProducMappingProfile));
        services.AddAutoMapper(typeof(UpdateProductMappingProfile));
        services.AddAutoMapper(typeof(GetProductMappingProfile));
        services.AddAutoMapper(typeof(GetProductByArticleMappingProfile));
        // Category
        services.AddAutoMapper(typeof(CreateCategoryMappingModel));
        services.AddAutoMapper(typeof(UpdateCategoryMappingProfile));
        services.AddAutoMapper(typeof(GetCategoryByIdMappingProfile));
        services.AddAutoMapper(typeof(GetCategoryByNameMappingProfile));
        // Seller
        services.AddAutoMapper(typeof(UpdateSellerMappingProfile));
        services.AddAutoMapper(typeof(CreateSellerMappingProfile));
        services.AddAutoMapper(typeof(GetAllSellersMappingProfile));
        services.AddAutoMapper(typeof(GetSellerByNameMappingProfile));
        // Review
        services.AddAutoMapper(typeof(CreateReviewMappingProfile));
        services.AddAutoMapper(typeof(UpdateReviewMappingProfile));
        // Order
        services.AddAutoMapper(typeof(CreateOrderMappingProfile));
        services.AddAutoMapper(typeof(UpdateOrderMappingProfile));
        services.AddAutoMapper(typeof(UpdateOrderMappingProfile));
        services.AddAutoMapper(typeof(GetOrderByIdMappingProfile));
        // Shopping Cart
        services.AddAutoMapper(typeof(AddToCartMappingProfile));
        
        services.AddValidatorsFromAssemblies([Assembly.GetExecutingAssembly()]);
        
        // Product
        services.AddMediatR(x =>
            x.RegisterServicesFromAssemblies(
                typeof(CreateClothesProductCommand).Assembly,
                typeof(CreateShoesProductCommand).Assembly,
                typeof(UpdateProductCommand).Assembly,
                typeof(DeleteProductCommand).Assembly,
                typeof(GetProductByIdQuery).Assembly,
                typeof(GetMiniProductListQuery).Assembly,
                typeof(GetProductByArticleQuery).Assembly,
                typeof(GetSortedProductsByPriceQuery).Assembly,
                typeof(GetSortedProductsByRatingQuery).Assembly,
                typeof(GetProductBySellerIdQuery).Assembly,
                typeof(GetProductByNoveltyQuery).Assembly,
                typeof(GetProductByNameQuery).Assembly
                ));
        // Category
        services.AddMediatR(x =>
            x.RegisterServicesFromAssemblies(
                typeof(CreateCategoryCommand).Assembly,
                typeof(UpdateCategoryCommand).Assembly,
                typeof(DeleteCategoryCommand).Assembly,
                typeof(GetCategoriesListQuery).Assembly,
                typeof(GetCategoryByIdQuery).Assembly,
                typeof(GetCategoryByNameQuery).Assembly
                ));
        // Seller
        services.AddMediatR(x =>
            x.RegisterServicesFromAssemblies(
                typeof(CreateSellerCommand).Assembly,
                typeof(UpdateSellerCommand).Assembly,
                typeof(DeleteSellerCommand).Assembly,
                typeof(GetAllSellersListQuery).Assembly,
                typeof(GetSellerByIdQuery).Assembly,
                typeof(GetSellerByNameQuery).Assembly
                ));
        // Review
        services.AddMediatR(x =>
            x.RegisterServicesFromAssemblies(
                typeof(CreateReviewCommand).Assembly,
                typeof(UpdateReviewCommand).Assembly,
                typeof(DeleteReviewCommand).Assembly,
                typeof(ModerateReviewCommand).Assembly,
                typeof(GetAllReviewsQuery).Assembly,
                typeof(GetAllReviewsByProductIdQuery).Assembly,
                typeof(GetAllReviewsByUserIdQuery).Assembly,
                typeof(GetFirstReviewsByDateQuery).Assembly,
                typeof(GetFirstReviewsByRatingQuery).Assembly,
                typeof(GetAllForModerationRequest).Assembly
                ));
        // Order
        services.AddMediatR(x =>
            x.RegisterServicesFromAssemblies(
                typeof(CreateOrderCommand).Assembly,
                typeof(UpdateOrderCommand).Assembly,
                typeof(DeleteOrderCommand).Assembly,
                typeof(GetAllOrdersQuery).Assembly,
                typeof(GetOrderByIdQuery).Assembly,
                typeof(GetOrdersByProductIdQuery).Assembly,
                typeof(GetOrdersByUserIdQuery).Assembly
                ));
        // Shopping Cart
        services.AddMediatR(x =>
            x.RegisterServicesFromAssemblies(
                typeof(AddToCartCommand).Assembly,
                typeof(RemoveFromCartCommand).Assembly,
                typeof(ClearCartCommand).Assembly,
                typeof(GetShoppingCartItemsByUserIdQuery).Assembly,
                typeof(GetAllCartItemsQuery).Assembly,
                typeof(GetTotalShoppingCartPriceQuery).Assembly
                ));

        services.AddMediatR(config => config.RegisterServicesFromAssembly(
            Assembly.GetExecutingAssembly()));
        
        return services;
    }
}