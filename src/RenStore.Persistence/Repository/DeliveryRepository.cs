using Dapper;
using Npgsql;
using RenStore.Application.Common.Exceptions;
using RenStore.Application.Repository;
using RenStore.Domain.Entities;

namespace RenStore.Persistence.Repository;

public class DeliveryRepository : IDeliveryRepository
{
    private readonly ApplicationDbContext context;
    private readonly string connectionString;
    
    public DeliveryRepository(ApplicationDbContext context)
    {
        this.context = context;
        this.connectionString = "";
    }
    
    public async Task<Guid> CreateAsync(Delivery delivery, CancellationToken cancellationToken)
    {
        await context.Deliveries.AddAsync(delivery, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        
        return delivery.Id;
    }

    public async Task UpdateAsync(Delivery model, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Delivery>> GetAllAsync(CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
            SELECT 
                * 
            FROM
                ""Delivery""";
        
        return await connection
            .QueryAsync<Delivery>(
                sql, cancellationToken)
                    ?? [];
    }

    public async Task<Delivery?> FindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"   
            SELECT 
                *
            FROM
                ""Delivery""
            WHERE
                ""Id"" = @id";
        
        return await connection
            .QueryFirstOrDefaultAsync<Delivery>(
                sql, new { Id = id });
    }

    public async Task<Delivery> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await FindByIdAsync(id, cancellationToken) 
            ?? throw new NotFoundException(typeof(Delivery), id);
    }
    
    public async Task<IEnumerable<Delivery>> FindByProductIdAsync(Guid productId, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"";

        return await connection.QueryAsync<Delivery>(
            sql, new 
            {
                ProductId = productId
            });
    }

    public async Task<IEnumerable<Delivery>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken)
    {
        return await this.GetByProductIdAsync(productId, cancellationToken)
            ?? throw new NotFoundException(typeof(Delivery), productId);
    }

    public async Task<IEnumerable<Delivery>> FindBySellerIdAsync(int sellerId, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
            SELECT
                *
            FROM
                ""Delivery""
            WHERE
                ""SellerId""=@SellerId
        ";
        
        return await connection.QueryAsync<Delivery>(
            sql, new
            {
                SellerId = sellerId
            });
    }

    public async Task<IEnumerable<Delivery>> GetBySellerIdAsync(int sellerId, CancellationToken cancellationToken)
    {
        return await this.FindBySellerIdAsync(sellerId, cancellationToken)
               ?? throw new NotFoundException(typeof(Delivery), sellerId);
    }

    public async Task<IEnumerable<Delivery>> FindByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
            SELECT
                *
            FROM
                ""Delivery""
            WHERE
                ""UserId""=@UserId
        ";
        
        return await connection.QueryAsync<Delivery>(
            sql, new
            {
                UserId = userId
            });
    }

    public async Task<IEnumerable<Delivery>> GetByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        return await this.FindByUserIdAsync(userId, cancellationToken)
            ?? throw new NotFoundException(typeof(Delivery), userId);
    }

}