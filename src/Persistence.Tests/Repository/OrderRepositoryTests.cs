/*using Microsoft.EntityFrameworkCore;
using Persistence.Tests.Common;
using RenStore.Domain.Entities;
using RenStore.Domain.Enums;
using RenStore.Persistence;
using RenStore.Persistence.Repository;

namespace Persistence.Tests.Repository;

public class OrderRepositoryTests : IDisposable
{
    private readonly ApplicationDbContext context;
    private readonly OrderRepository repository;
    
    private readonly string connectionString = "Server=localhost;Port=5432;DataBase=UnitTestsDb; User Id=re;Password=postgres ;Include Error Detail=True";

    public OrderRepositoryTests()
    {
        context = TestContextFactory.CreateReadyContext(connectionString);
        repository = new OrderRepository(context, connectionString);
    }

    [Fact]
    public async Task CreateOrderAsync_Success_Test()
    {
        // Arrange
        string userId = Guid.NewGuid().ToString();
        Guid productId = Guid.NewGuid();
        var order = new Order()
        {
            Id = Guid.NewGuid(),
            Address = "Sample Address",
            City = "Sample City",
            Country = "Sample Country",
            Amount = 1,
            ZipCode = 111111,
            Status = DeliveryStatus.AwaitingConfirmation,
            OrderTotal = 321312,
            CreatedDate = DateTime.UtcNow,
            ApplicationUserId = userId,
            ProductId = productId
        };
        // Act
        await repository.CreateAsync(order, CancellationToken.None);
        // Assert
        var result = await context.Orders
            .FirstOrDefaultAsync(o => o.Id == order.Id);
        Assert.NotNull(result);
        Assert.Equal(order.Id, result.Id);
        Assert.Equal(order.Address, result.Address);
    }

    [Fact]
    public async Task DeleteOrderAsync_Success_Test()
    {
        // Arrange
        // Act
        await repository.DeleteAsync(
            TestContextFactory.OrderIdForDelete,
            CancellationToken.None);
        // Assert
        var result = await context.Orders
            .FirstOrDefaultAsync(o => 
                o.Id == TestContextFactory.OrderIdForDelete);
        Assert.Null(result);
    }
    
    public void Dispose()
    {
        
    }
}*/