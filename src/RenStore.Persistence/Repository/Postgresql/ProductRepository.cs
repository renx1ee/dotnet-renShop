using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using RenStore.Application.Common.Exceptions;
using RenStore.Domain.Entities;
using RenStore.Domain.Enums.Sorting;
using RenStore.Domain.Repository;

namespace RenStore.Persistence.Repository.Postgresql;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;
    private readonly string _connectionString;

    private readonly Dictionary<ProductSortBy, string> _sortColumnMapping = new()
    {
        { ProductSortBy.Id, "product_id" }
    };

    public ProductRepository(
        ApplicationDbContext context,
        string connectionString)
    {
        this._context = context;
        this._connectionString = connectionString 
            ?? throw new ArgumentNullException(nameof(connectionString));
    }

    public ProductRepository(
        ApplicationDbContext context,
        IConfiguration configuration)
    {
        this._context = context;
        this._connectionString = configuration
            .GetConnectionString("DefaultConnection")
            ?? throw new ArgumentNullException(nameof(_connectionString));
    }

    public async Task<Guid> CreateAsync(
        ProductEntity product,
        CancellationToken cancellationToken)
    {
        var result = await _context.Products.AddAsync(product, cancellationToken);
        await this._context.SaveChangesAsync(cancellationToken);
        return result.Entity.Id;
    }
    
    public async Task UpdateAsync(
        ProductEntity product,
        CancellationToken cancellationToken)
    {
        var existingProduct = await this.GetByIdAsync(product.Id, cancellationToken);
        this._context.Products.Update(product);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task DeleteAsync(
        ProductEntity product,
        CancellationToken cancellationToken)
    {
        var existingProduct = await this.GetByIdAsync(product.Id, cancellationToken);
        this._context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<ProductEntity>> FindAllAsync(
        CancellationToken cancellationToken,
        uint pageCount = 25,
        uint page = 1,
        bool descending = false,
        ProductSortBy sortBy = ProductSortBy.Id)
    {
        try
        {
            pageCount = Math.Min(pageCount, 1000);
            uint offset = (page - 1) * pageCount;
            var direction = descending ? "DESC" : "ASC";
            var columnName = _sortColumnMapping.GetValueOrDefault(sortBy, "product_id");
            
            await using var connection = new NpgsqlConnection(this._connectionString);
            await connection.OpenAsync(cancellationToken);

            string sql =
                $@"
                    SELECT
                        ""product_id"" AS Id,
                        ""is_blocked"" AS IsBlocked,
                        ""overall_rating"" AS OverallRating,
                        ""seller_id"" AS SellerId,
                        ""category_id"" AS CategoryId
                    FROM
                        ""products""
                    ORDER BY
                        {columnName} {direction}
                    LIMIT @Count
                    OFFSET @Offset;
                ";

            return await connection
                .QueryAsync<ProductEntity>(
                    sql, new
                    {
                        Count = (int)pageCount,
                        Offset = (int)offset
                    });
        }
        catch (PostgresException e)
        {
            throw new Exception($"Database error occured: {e.Message}");
        }
    }
    
    public async Task<ProductEntity?> FindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await using var connection = new NpgsqlConnection(this._connectionString);
            await connection.OpenAsync(cancellationToken);

            const string sql = 
                @"
                    SELECT
                        ""product_id"" AS Id,
                        ""is_blocked"" AS IsBlocked,
                        ""overall_rating"" AS OverallRating,
                        ""seller_id"" AS SellerId,
                        ""category_id"" AS CategoryId
                    FROM
                        ""products""
                    WHERE
                        ""product_id"" = @Id;
                ";

            return await connection
                .QueryFirstOrDefaultAsync<ProductEntity>(
                    sql, new { Id = id});
        }
        catch (PostgresException e)
        {
            throw new Exception($"Database error occured: {e.Message}");
        }
    }

    public async Task<ProductEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await this.FindByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException(typeof(ProductEntity), id);
    }
}