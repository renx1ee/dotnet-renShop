using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RenStore.Persistence.Features.ProductQuestion.Queries.GetAllByProductId;
using RenStore.Persistence.Features.ProductQuestion.Queries.GetAllByUserId;
using RenStore.Persistence.Features.ProductQuestion.Queries.GetById;

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
                typeof(ModerateReviewCommandHandler).Assembly,
                typeof(GetAllCategoriesQueryHandler).Assembly,
                typeof(GetAllReviewsByProductIdQueryHandler).Assembly,
                typeof(GetAllReviewsByUserIdQueryHandler).Assembly,
                typeof(GetAllForModerationRequestHandler).Assembly)
            );
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
        
        // Question
        services.AddMediatR(x =>
            x.RegisterServicesFromAssemblies(
                typeof(CreateProductQuestionCommandHandler).Assembly,
                typeof(DeleteProductQuestionCommandHandler).Assembly,
                typeof(GetAllQuestionsCommandHandler).Assembly,
                typeof(GetProductQuestionByIdQueryHandler).Assembly,
                typeof(GetQuestionWithAnswerByIdQueryHandler).Assembly,
                typeof(GetAllQuestionsByUserIdQueryHandler).Assembly,
                typeof(GetAllQuestionsByProductIdQueryHandler).Assembly
            ));
        
        // Answer
        services.AddMediatR(x =>
            x.RegisterServicesFromAssemblies(
                typeof(CreateProductAnswerCommandHandler).Assembly,
                typeof(DeleteProductAnswerCommandHandler).Assembly
            ));
         
        return services;
    }
}