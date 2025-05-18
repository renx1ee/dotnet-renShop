using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using RenStore.Application.Data.Common.Exceptions;
using RenStore.Application.Repository;
using RenStore.Domain.Entities;

namespace RenStore.Persistence.Repository;

public class ReviewRepository : IReviewRepository
{
    private readonly ApplicationDbContext context;
    private readonly string? connectionString;
    public ReviewRepository(
        ApplicationDbContext context,
        IConfiguration configuration) 
    {
        connectionString = configuration.GetConnectionString("DefaultConnection");
        this.context = context;
    }
        
    public async Task<Guid> CreateAsync(Review review, 
        CancellationToken cancellationToken)
    {
        await context.Reviews.AddAsync(review, cancellationToken);
        await context.SaveChangesAsync();
        return review.Id;    
    }

    public async Task UpdateAsync(Review review, CancellationToken cancellationToken)
    {
        Review result = await GetByIdAsync(review.Id, cancellationToken)
            ?? throw new NotFoundException(typeof(Review), review.Id);
        
        context.Reviews.Update(review);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        Review review = await GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException(typeof(Review), id);
        
        context.Reviews.Remove(review);
        await context.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<Review>?> GetAllAsync(CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
            SELECT
                *
            FROM
                ""Reviews""";

        return await connection
            .QueryAsync<Review>(
                sql, cancellationToken)
                    ?? null;
    }

    public async Task<Review?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken)
    {
        return await this.FindByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException(typeof(Review), id);
    }

    public async Task<Review?> FindByIdAsync(Guid id, 
        CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);
        
        const string sql = @"
            SELECT
                *
            FROM
                ""Reviews""
            WHERE
                ""Id""=@Id";

        return await connection
            .QueryFirstOrDefaultAsync<Review>(
                sql, new { Id = id })
                    ?? null;
    }

    public async Task<IEnumerable<Review>> GetByUserIdAsync(string userId,
        CancellationToken cancellationToken)
    {
        return await this.FindByUserIdAsync(userId, cancellationToken)
            ?? throw new NotFoundException(typeof(Review), userId);
    }

    public async Task<IEnumerable<Review>?> FindByUserIdAsync(string userId, 
        CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
            SELECT
                *
            FROM
                ""Reviews""
            WHERE
                ""ApplicationUserId""=@UserId";
        
        return await connection
            .QueryAsync<Review>(
                sql, new { UserId = userId })
                    ?? null;
    }
    
    public async Task<IEnumerable<Review>> GetByProductIdAsync(Guid productId,
        CancellationToken cancellationToken)
    {
        return await this.FindByProductIdAsync(productId, cancellationToken)
            ?? throw new NotFoundException(typeof(Review), productId);
    }
    
    public async Task<IEnumerable<Review>?> FindByProductIdAsync(Guid productId, 
        CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
            SELECT
                *
            FROM
                ""Reviews""
            WHERE
                ""ProductId""=@ProductId";
        
        return await connection
           .QueryAsync<Review>(
               sql, new
               {
                   ProductId = productId
               })
                   ?? null;
    }

    public async Task<IEnumerable<Review>?> FindByUserIdAndProductIdAsync(
        string userId,
        Guid productId,
        CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
        SELECT
            *
            FROM
                ""Reviews""
        WHERE
            ""ApplicationUserId""=@UserId
        AND 
            ""ProductId""=@ProductId";
        
        return await connection
           .QueryAsync<Review>(
               sql, new
               {
                   UserId = userId,
                   ProductId = productId
               })
               ?? null;
    }
    
    
    /*public async Task<IEnumerable<Review>> GetFirstByDateLineAsync(
        int count, 
        Guid productId, 
        DateTime dateStart, 
        DateTime dateEnd, 
        CancellationToken cancellationToken)
    {
        return await context.Reviews
            .Where(review => review.ProductId == productId
                && review.CreatedDate.Date.CompareTo(dateStart.Date) >= 0 
                && review.CreatedDate.Date.CompareTo(dateEnd.Date) <= 0)
            .Take(count)
            .ToListAsync(cancellationToken)
            ?? throw new NotFoundException(typeof(Review), productId);
    }*/
    // TODO: Make with dapper
    public async Task<IEnumerable<Review>> GetFirstByDateAsync(
        int count, 
        Guid productId, 
        CancellationToken cancellationToken)
    {
        return await context.Reviews
            .Where(review => review.ProductId == productId
                && review.IsApproved == true)
            .OrderBy(review => review.CreatedDate.Date)
            .Take(count)
            .ToListAsync(cancellationToken)
            ?? throw new NotFoundException(typeof(Review), productId);
    }
    // TODO: Make with dapper
    public async Task<IEnumerable<Review>> GetFirstByRatingAsync(int count, Guid productId, 
        CancellationToken cancellationToken)
    {
        return await context.Reviews
            .Where(review => review.ProductId == productId
                && review.IsApproved == true)
            .OrderBy(review => review.Rating)
            .Take(count)
            .ToListAsync(cancellationToken)
            ?? throw new NotFoundException(typeof(Review), productId);
    }
    // TODO: Make with dapper
    public async Task<bool> CheckExistByUserIdAsync(Guid productId, string userId, 
        CancellationToken cancellationToken)
    {
        var result = await context.Reviews
            .FirstOrDefaultAsync(review =>
                review.ProductId == productId &&
                review.ApplicationUserId == userId,
                cancellationToken);
            
        return true;
    }
    public async Task LikeAsync(Guid reviewId, CancellationToken cancellationToken)
    {
        var review = await GetByIdAsync(reviewId, cancellationToken);
        review.LikesCount += 1;
        
        await UpdateAsync(review, cancellationToken);
    }
}
