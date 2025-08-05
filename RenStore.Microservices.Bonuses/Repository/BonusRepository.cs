using Dapper;
using Npgsql;
using RenStore.Microservices.Bonuses.Models;

namespace RenStore.Microservices.Bonuses.Repository;

public class BonusRepository : IBonusRepository
{
    private readonly BonusDbContext context;
    private readonly string connectionString;

    public BonusRepository(
        BonusDbContext context, 
        IConfiguration configuration)
    {
        this.context = context;
        this.connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }
    
    public async Task CreateAsync(Bonus bonus, CancellationToken cancellationToken)
    {
        await context.AddAsync(bonus, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task UpdateAsync(Bonus bonus, CancellationToken cancellationToken)
    {
        context.Update(bonus);
        await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        context.Remove(cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Bonus?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"";

        return await connection
            .QueryFirstOrDefaultAsync(
                 sql, new { Id = id })
                    ?? null;
    }
    
    public async Task<IEnumerable<Bonus>> GetAllAsync(CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"";

        return await connection
            .QueryAsync<Bonus>(sql)
                ?? [];
    }
    
    public async Task<IEnumerable<Bonus>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"";

        return await connection
            .QueryAsync<Bonus>(
                sql, new 
                {
                    UserId = userId
                })
                    ?? [];
    }
    
    public async Task<IEnumerable<Bonus>> GetBySellerIdAsync(Guid sellerId, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"";
        
        return await connection 
            .QueryAsync<Bonus>(
                sql, new
                {
                    SellerId = sellerId
                })
                    ?? [];
    }
    
    public async Task<IEnumerable<Bonus>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"";
        
        return await connection 
           .QueryAsync<Bonus>(
               sql, new
               {
                   ProductId = productId
               })
                   ?? [];
    }
}