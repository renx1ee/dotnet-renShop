using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using RenStore.Application.Common.Exceptions;
using RenStore.Application.Repository;
using RenStore.Domain.Entities;

namespace RenStore.Persistence.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext context;
    private readonly string? connectionString;

    public OrderRepository(
        ApplicationDbContext context,
        IConfiguration configuration)
    {
        this.context = context;
        connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    
    public OrderRepository(
        ApplicationDbContext context,
        string connectionString)
    {
        this.context = context;
        this.connectionString = connectionString;
    }

    public async Task<Guid> CreateAsync(Order order, CancellationToken cancellationToken)
    {
        await context.Orders.AddAsync(order, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return order.Id;    
    }

    public async Task UpdateAsync(Order order, CancellationToken cancellationToken)
    {
        Order result = await this.GetByIdAsync(order.Id, cancellationToken)
            ?? throw new NotFoundException(typeof(Order), order.Id);
        
        context.Orders.Update(order);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        Order order = await GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException(typeof(Order), id);
        
        context.Orders.Remove(order);
        await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"SELECT * FROM ""Orders""";
        
        return await connection
            .QueryAsync<Order>(
                sql, cancellationToken)
                    ?? [];
    }
    
    public async Task<Order?> FindByIdAsync(Guid id, 
        CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"SELECT * FROM ""Orders"" WHERE ""Id""=@Id";

        return await connection
           .QueryFirstOrDefaultAsync<Order>(
               sql, new { Id = id})
                   ?? null;
    }

    public async Task<Order> GetByIdAsync(Guid id, 
        CancellationToken cancellationToken)
    {
        return await this.FindByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException(typeof(Order), id);
    }

    public async Task<IEnumerable<Order>?> FindByUserIdAsync(string userId,
        CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"SELECT * FROM ""Orders"" WHERE ""ApplicationUserId""=@UserId";

        return await connection
           .QueryAsync<Order>(
               sql, new { UserId = userId })
                   ?? null;
    }

    public async Task<IEnumerable<Order>> GetByUserIdAsync(string userId, 
        CancellationToken cancellationToken)
    {
        return await this.FindByUserIdAsync(userId, cancellationToken)
            ?? throw new NotFoundException(typeof(Order), userId);
    }
    
    public async Task<IEnumerable<Order>?> FindByProductIdAsync(Guid productId, 
        CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"SELECT * FROM ""Orders"" WHERE ""ProductId""=@ProductId";

        return await connection
           .QueryAsync<Order>(
               sql, new { ProductId = productId })
                   ?? null;
    }
    
    public async Task<IEnumerable<Order>> GetByProductIdAsync(Guid productId, 
        CancellationToken cancellationToken)
    {
        return await this.FindByProductIdAsync(productId, cancellationToken)
            ?? throw new NotFoundException(typeof(Order), productId);
    }
}