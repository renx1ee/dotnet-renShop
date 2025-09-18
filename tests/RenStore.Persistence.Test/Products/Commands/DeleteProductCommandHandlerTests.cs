/*using Microsoft.Extensions.Logging;
using RenStore.Application.Entities.Product.Commands.Delete;
using RenStore.Application.Repository;
using RenStore.Persistence.Entities.Product.Command.Delete;
using RenStore.Persistence.Repository;
using RenStore.Persistence.Test.Common;

namespace RenStore.Persistence.Test.Products.Commands;

public class DeleteProductCommandHandlerTests : TestCommandBase
{
    private IProductRepository productRepository;
    private readonly ILogger<DeleteProductCommandHandler> logger;
    
    public DeleteProductCommandHandlerTests(
        ILogger<DeleteProductCommandHandler> logger) => 
        (this.logger) = (logger);
    
    [Fact]
    public async Task DeleteProductCommandHandler_Success_Test()
    {
        // Arrange
        productRepository = new ProductRepository(context);
        var handler = new DeleteProductCommandHandler(productRepository, logger);
        // Act
        await handler.Handle(new DeleteProductCommand
        { ProductId = ProductContextFactory.ProductIdForDelete },
        CancellationToken.None);

        var result = productRepository.GetByIdAsync(
            ProductContextFactory.ProductIdForDelete,
            CancellationToken.None);
        // Assert
        Assert.Null(result);
    }

    /*[Fact]
    public async Task DeleteProductCommandHandlerTest_FailOnWrongId()
    {
        // Arrange
        var handler = new DeleteProductCommandHandler(context);
        
        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(
                new DeleteProductCommand
                {
                    ProductId = Guid.NewGuid(),
                    UserId = ProductContextFactory.UserIdA
                }, 
                CancellationToken.None));
    }

    [Fact]
    public async Task DeleteProductCommandHandlerTest_FailOnWrongUserId()
    {
        // Arrange
        var deleteHandler = new DeleteProductCommandHandler(context);
        var createHandler = new CreateProductCommandHandler(context);
        var productId = await createHandler.Handle(
            new CreateClothesProductCommand
            {
                ProductName = "",
                Description = "",
            }, 
            CancellationToken.None);

        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await deleteHandler.Handle(
                new DeleteProductCommand
                {
                    ProductId = productId,
                    UserId = ProductContextFactory.UserIdB
                }, 
                CancellationToken.None));
    }#1#
}*/