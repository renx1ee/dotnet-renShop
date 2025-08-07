using System.Text;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using RenStore.Application.Common.Exceptions;
using RenStore.Application.Repository;
using RenStore.Domain.Entities;
using RenStore.Domain.Enums;

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
        await context.SaveChangesAsync(cancellationToken);
    }
    
    // TODO: 
    public async Task<IEnumerable<Review>?> GetAllAsync(ReviewStatusFilter status, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        var sql = new StringBuilder(@"
            SELECT
                *
            FROM
                ""Reviews""");

        switch (status)
        {
            case ReviewStatusFilter.All:
                break;
            case ReviewStatusFilter.Published:
                sql.Append(@"WHERE ""Status""=0");
                break;
            case ReviewStatusFilter.SentForModeration:
                sql.Append(@"WHERE ""Status""=2");
                break;
            case ReviewStatusFilter.Rejected:
                sql.Append(@"WHERE ""Status""=3");
                break;
        }
        
        return await connection
            .QueryAsync<Review>(
                sql.ToString(), 
                cancellationToken)
                    ?? null;
    }
    // TODO: убрать, т.к. метод выше может так же
    public async Task<IEnumerable<Review>?> GetAllForModerationAsync(CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
            SELECT
                *
            FROM
                ""Reviews""
            WHERE 
                ""Status""=2";

        return await connection
           .QueryAsync<Review>(
               sql, cancellationToken)
                   ?? null;
    }

    public async Task<Review> GetByIdAsync(Guid id,
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
    
    public async Task<IEnumerable<Review>> GetByUserIdAsync(
        ReviewStatusFilter status, 
        string userId,
        CancellationToken cancellationToken)
    {
        return await this.FindByUserIdAsync(status, userId, cancellationToken)
            ?? throw new NotFoundException(typeof(Review), userId);
    }
    // TODO:
    public async Task<IEnumerable<Review>?> FindByUserIdAsync(
        ReviewStatusFilter status, 
        string userId, 
        CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);
        
        var sql = new StringBuilder(@"
            SELECT
                *
            FROM
                ""Reviews""
            WHERE
                ""ApplicationUserId""=@UserId");
        
        switch (status)
        {
            case ReviewStatusFilter.All:
                break;
            case ReviewStatusFilter.Published:
                sql.Append(@"AND ""Status""=0");
                break;
            case ReviewStatusFilter.SentForModeration:
                sql.Append(@"AND ""Status""=2");
                break;
            case ReviewStatusFilter.Rejected:
                sql.Append(@"AND ""Status""=3");
                break;
        }
        
        return await connection
            .QueryAsync<Review>(
                sql.ToString(), 
                new { UserId = userId })
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
                && review.Status == ReviewStatus.Published)
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
                && review.Status == ReviewStatus.Published)
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

        if (result is null)
            return false;
            
        return true;
    }
    public async Task LikeAsync(Guid reviewId, CancellationToken cancellationToken)
    {
        var review = await GetByIdAsync(reviewId, cancellationToken);
        review.LikesCount += 1;
        
        await UpdateAsync(review, cancellationToken);
    }
}
