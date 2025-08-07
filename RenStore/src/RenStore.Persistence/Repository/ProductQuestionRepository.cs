using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using RenStore.Application.Common.Exceptions;
using RenStore.Application.Repository;
using RenStore.Domain.Entities;

namespace RenStore.Persistence.Repository;

public class ProductQuestionRepository : IProductQuestionRepository
{
    private readonly ApplicationDbContext context;
    private readonly string connectionString;

    public ProductQuestionRepository(
        ApplicationDbContext context,
        IConfiguration configuration)
    {
        this.context = context;
        connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public async Task<Guid> CreateAsync(ProductQuestion question, CancellationToken cancellationToken)
    {
        await context.AddAsync(question, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return question.Id;
    }

    public async Task UpdateAsync(ProductQuestion question, CancellationToken cancellationToken)
    {
        var result = await this.GetByIdAsync(question.Id, cancellationToken);

        context.ProductQuestions.Update(question);
        await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task DeleteAsync(Guid questionId, CancellationToken cancellationToken)
    {
        var question = await this.GetByIdAsync(questionId, cancellationToken);

        context.Remove(question);
        await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<ProductQuestion>> GetAllAsync(CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"SELECT * FROM ""ProductQuestions""";

        return await connection
            .QueryAsync<ProductQuestion>(
                sql, cancellationToken)
                    ?? [];
    }

    public async Task<ProductQuestion?> FindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
            SELECT 
                * 
            FROM 
                ""ProductQuestions""
            WHERE
                ""Id""=@Id";

        return await connection
            .QueryFirstOrDefaultAsync<ProductQuestion>(
                sql, new { Id = id })
                    ?? null;
    }

    public async Task<ProductQuestion?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await this.FindByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException(typeof(Order), id);
    }
    
    public async Task<IEnumerable<ProductQuestion>> FindByProductIdAsync(Guid productId, uint count, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
            SELECT
                *
            FROM
                ""ProductQuestion""
            WHERE
                ""ProductId"" = @ProductId 
            AND
                LIMIT = @Count";

        return await connection
            .QueryAsync<ProductQuestion>(
                sql, new
                {
                    ProductId = productId, 
                    Count = count
                }) 
                    ?? [];
    }
    
    public async Task<IEnumerable<ProductQuestion>> GetByProductIdAsync(Guid productId, uint count,
        CancellationToken cancellationToken)
    {
        return await this.FindByProductIdAsync(productId, count, cancellationToken)
            ?? throw new NotFoundException(typeof(ProductQuestion), productId);
    }
    
    public async Task<IEnumerable<ProductQuestion>> FindByUserIdAsync(string userId, uint count, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection();
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
            SELECT
                *
            FROM
                ""ProductQuestion""
            WHERE
                ""ApplicationUserId"" = @UserId 
            AND
                LIMIT = @Count";
        
        return await connection
            .QueryAsync<ProductQuestion>(
                sql, new
                {
                    UserId = userId,
                    Count = count
                }) 
                    ?? [];
    }

    public async Task<IEnumerable<ProductQuestion>> GetByUserIdAsync(string userId, uint count,
        CancellationToken cancellationToken)
    {
        return await this.FindByUserIdAsync(userId, count, cancellationToken)
            ?? throw new NotFoundException(typeof(ProductQuestion), userId);
    }
}