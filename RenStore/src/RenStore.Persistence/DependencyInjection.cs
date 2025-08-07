using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RenStore.Application.Features.Category.Queries.GetAllCategories;
using RenStore.Application.Features.Category.Queries.GetCategoryById;
using RenStore.Application.Features.Orders.Commands.Create;
using RenStore.Application.Features.Orders.Commands.Delete;
using RenStore.Application.Features.Orders.Commands.Update;
using RenStore.Application.Features.Orders.Queries.GetById;
using RenStore.Application.Features.Orders.Queries.GetByProductId;
using RenStore.Application.Features.Orders.Queries.GetByUserId;
using RenStore.Application.Features.Product.Commands.Create.Clothes;
using RenStore.Application.Features.Product.Commands.Create.Shoes;
using RenStore.Application.Features.Product.Commands.Update;
using RenStore.Application.Features.Product.Queries.GetByArticle;
using RenStore.Application.Features.Product.Queries.GetById;
using RenStore.Application.Features.Product.Queries.GetByNovelty;
using RenStore.Application.Features.Product.Queries.GetBySellerId;
using RenStore.Application.Features.Product.Queries.GetMinimizedProducts;
using RenStore.Application.Features.Product.Queries.GetSortedByPrice;
using RenStore.Application.Features.Product.Queries.GetSortedByRating;
using RenStore.Application.Features.ProductAnswer.Command.Create;
using RenStore.Application.Features.ProductAnswer.Command.Delete;
using RenStore.Application.Features.ProductAnswer.Queries.GetById;
using RenStore.Application.Features.ProductQuestion.Command.Create;
using RenStore.Application.Features.ProductQuestion.Command.Delete;
using RenStore.Application.Features.ProductQuestion.Queries.GetAll;
using RenStore.Application.Features.ProductQuestion.Queries.GetAllByProductId;
using RenStore.Application.Features.ProductQuestion.Queries.GetAllByUserId;
using RenStore.Application.Features.ProductQuestion.Queries.GetById;
using RenStore.Application.Features.ProductQuestion.Queries.GetQuestionWithAnswerById;
using RenStore.Application.Features.Seller.Command.Create;
using RenStore.Application.Features.Seller.Command.Delete;
using RenStore.Application.Features.Seller.Command.Update;
using RenStore.Application.Features.Seller.Queries.GetAll;
using RenStore.Application.Features.Seller.Queries.GetByName;
using RenStore.Application.Features.ShoppingCart.Command.Add;
using RenStore.Application.Features.ShoppingCart.Command.Clear;
using RenStore.Application.Features.ShoppingCart.Command.Remove;
using RenStore.Application.Features.ShoppingCart.Query.GetAll;
using RenStore.Application.Features.ShoppingCart.Query.GetByUserId;
using RenStore.Application.Features.ShoppingCart.Query.GetTotalPrice;

namespace RenStore.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services;
    }
}