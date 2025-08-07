using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using RenStore.Application.Common.Exceptions;
using RenStore.Application.Repository;
using RenStore.Domain.Entities;

namespace RenStore.Persistence.Repository;

public class ProductAnswerRepository : IProductAnswerRepository
{
    private readonly ApplicationDbContext context;
    private readonly string connectionString;

    public ProductAnswerRepository(
        ApplicationDbContext context,
        IConfiguration configuration)
    {
        this.context = context;
        connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public async Task<Guid> CreateAsync(ProductAnswer answer, CancellationToken cancellationToken)
    {
        await context.AddAsync(answer, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return answer.Id;
    }

    public async Task UpdateAsync(ProductAnswer answer, CancellationToken cancellationToken)
    {
        var result = await this.GetByIdAsync(answer.Id, cancellationToken);

        context.ProductAnswers.Update(answer);
        await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var answer = await this.GetByIdAsync(id, cancellationToken);

        context.Remove(answer);
        await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<ProductAnswer>> GetAllAsync(CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"SELECT * FROM ""ProductAnswers""";

        return await connection
            .QueryAsync<ProductAnswer>(
                sql, cancellationToken)
                    ?? [];
    }

    public async Task<ProductAnswer?> FindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
            SELECT 
                * 
            FROM 
                ""ProductAnswers""
            WHERE
                ""Id""=@Id";

        return await connection
            .QueryFirstOrDefaultAsync<ProductAnswer>(
                sql, new { Id = id })
                    ?? null;
    }

    public async Task<ProductAnswer?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await this.FindByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException(typeof(Order), id);
    }
    
    public async Task<IEnumerable<ProductAnswer>> FindByProductIdAsync(Guid productId, uint count, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
            SELECT
                *
            FROM
                ""ProductAnswers""
            WHERE
                ""ProductId"" = @ProductId 
            AND
                LIMIT = @Count";

        return await connection
            .QueryAsync<ProductAnswer>(
                sql, new
                {
                    ProductId = productId, 
                    Count = count
                }) 
                    ?? [];
    }
    
    public async Task<IEnumerable<ProductAnswer>> GetByProductIdAsync(Guid productId, uint count, CancellationToken cancellationToken)
    {
        return await this.FindByProductIdAsync(productId, count, cancellationToken)
            ?? throw new NotFoundException(typeof(ProductAnswer), productId);
    }
    
    public async Task<IEnumerable<ProductAnswer>> FindByUserIdAsync(string userId, uint count, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection();
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
            SELECT
                *
            FROM
                ""ProductAnswers""
            WHERE
                ""ApplicationUserId"" = @UserId 
            AND
                LIMIT = @Count";
        
        return await connection
            .QueryAsync<ProductAnswer>(
                sql, new
                {
                    UserId = userId,
                    Count = count
                }) 
                    ?? [];
    }

    public async Task<IEnumerable<ProductAnswer>> GetByUserIdAsync(string userId, uint count, CancellationToken cancellationToken)
    {
        return await this.FindByUserIdAsync(userId, count, cancellationToken)
            ?? throw new NotFoundException(typeof(ProductAnswer), userId);
    }
    
    public async Task<ProductAnswer?> FindByQuestionIdAsync(Guid questionId, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection();
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
            SELECT
                *
            FROM
                ""ProductAnswers""
            WHERE
                ""ProductQuestionId"" = @ProductQuestionId";
        
        return await connection
           .QueryFirstOrDefaultAsync(
               sql, new
               {
                   ProductQuestionId = questionId,
               }) 
                   ?? null;
    }

    public async Task<ProductAnswer> GetByQuestionIdAsync(Guid questionId, CancellationToken cancellationToken)
    {
        return await this.FindByQuestionIdAsync(questionId, cancellationToken)
            ?? throw new NotFoundException(typeof(ProductAnswer), questionId);
    }
    
    public async Task<ProductAnswer?> FindBySellerIdAsync(Guid sellerId, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection();
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
            SELECT
                *
            FROM
                ""ProductAnswers""
            WHERE
                ""SellerId"" = @SellerId";
        
        return await connection
                   .QueryFirstOrDefaultAsync(
                       sql, new
                       {
                           SellerId = sellerId,
                       }) 
               ?? null;
    }

    public async Task<ProductAnswer> GetBySellerIdAsync(Guid sellerId, CancellationToken cancellationToken)
    {
        return await this.FindBySellerIdAsync(sellerId, cancellationToken)
               ?? throw new NotFoundException(typeof(ProductAnswer), sellerId);
    }
}