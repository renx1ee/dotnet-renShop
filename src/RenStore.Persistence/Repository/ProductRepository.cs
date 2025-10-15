using System.Text;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using RenStore.Application.Common.Exceptions;
using RenStore.Application.Repository;
using RenStore.Domain.Entities;
using RenStore.Domain.Entities.Products;

namespace RenStore.Persistence.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext context;
    private readonly string? connectionString;
    
    public ProductRepository(
        ApplicationDbContext context,
        IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("DefaultConnection");
        this.context = context;
    }
    
    public ProductRepository(
        ApplicationDbContext context,
        string connectionString)
    {
        this.connectionString = connectionString;
        this.context = context;
    }

    public async Task<Guid> CreateAsync(Product product, CancellationToken cancellationToken)
    {
        await context.Products.AddAsync(product, cancellationToken);
            
        product.ProductDetailId = product.ProductDetail.Id;
        
        var validProduct = await ProductValidCheckerAsync(product)
            ?? throw new NotFoundException(typeof(Product), product.Id);
            
        await context.SaveChangesAsync(cancellationToken);
        
        return product.Id;
     }

    private async Task<Product> ProductValidCheckerAsync(Product product)
    {
        if (product.ClothesProduct != null)
        {
            product.ClothesProductId = product.ClothesProduct.Id;
            return product;
        }
        else if (product.ShoesProduct != null)
        {
            product.ShoesProductId = product.ShoesProduct.Id;
            return product;
        }
        return null;
    }

    public async Task UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        var model = await GetByIdAsync(product.Id, cancellationToken)
            ?? throw new NotFoundException(typeof(Product), product.Id);
        
        context.Entry<Product>(model).State = EntityState.Detached;
        context.Products.Update(product);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException(typeof(Product), id);
        
        context.Products.Remove(product);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
            SELECT 
                ""Id"", 
                ""ProductName"", 
                ""Price"", 
                ""Rating"", 
                ""ImageMiniPath"", 
                ""SellerName"" 
            FROM 
                ""Products""";

        return await connection
            .QueryAsync<Product>(
                sql, cancellationToken) 
                    ?? [];
    }
    
    public async Task<Product> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await this.FindByIdAsync(id, cancellationToken)
               ?? throw new NotFoundException(typeof(Product), id);
    }

    public async Task<Product?> FindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        string sql = @"
            SELECT 
                * 
            FROM 
                ""Products""
            WHERE 
                ""Id""=@Id";

        return await connection
           .QueryFirstOrDefaultAsync<Product>(
               sql, new { Id = id })
                   ?? null;
    }
    
    public async Task<ClothesProduct?> GetClothesByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await context.ClothesProducts
            .FirstOrDefaultAsync(product => 
                product.Id == id, 
                cancellationToken)
            ?? throw new NotFoundException(typeof(ClothesProduct), id);

        product.Product = null;
        return product;
    }
    
    public async Task<ShoesProduct?> GetShoesByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await context.ShoesProducts
            .FirstOrDefaultAsync(product => 
                product.Id == id, 
                cancellationToken)
            ?? throw new NotFoundException(typeof(ShoesProduct), id);
        
        product.Product = null;
        return product;
    }

    public async Task<ProductDetail?> GetDetailsByProductIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await this.FindDetailsByProductIdAsync(id, cancellationToken)
               ?? throw new NotFoundException(typeof(ProductDetail), id);
    }
    public async Task<ProductDetail?> FindDetailsByProductIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
            SELECT 
                *
            FROM 
                ""ProductDetails""
            WHERE 
                ""ProductId""=@Id";
        
        return await connection
            .QueryFirstOrDefaultAsync<ProductDetail>(
                sql, new { Id = id })
                    ?? null;
    }

    public async Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId, CancellationToken cancellationToken)
    {
        return await this.FindByCategoryIdAsync(categoryId, cancellationToken)
            ?? throw new NotFoundException(typeof(Product), categoryId);
    }

    public async Task<IEnumerable<Product>?> FindByCategoryIdAsync(int categoryId, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
            SELECT
                *
            FROM
                ""Products""
            WHERE
                ""CategoryId""=@CategoryId";
        
        return await connection
            .QueryAsync<Product>(
                sql, new
                {
                    CategoryId = categoryId
                })
                    ?? null;
    }

    public async Task<ProductDetail?> FindDetailByArticleIdAsync(int article, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        string sql = @"
            SELECT
                *
            FROM
                ""ProductDetails""
            WHERE
                ""Article""=@Article";
        
        return await connection
           .QueryFirstOrDefaultAsync<ProductDetail>(
               sql, new
               {
                   Article = article
               })
                   ?? null;
    }

    public async Task<IEnumerable<Product>> FindSortedByPriceAsync(
        bool descending,
        CancellationToken cancellationToken,
        decimal? maxPrice = null, 
        decimal? minPrice = null)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);
        
        var direction = descending ? "DESC" : "ASC";
        
        var sql = new StringBuilder(@"
            SELECT 
                ""Id"", 
                ""ProductName"", 
                ""Price"", 
                ""Rating"", 
                ""ImageMiniPath"", 
                ""SellerName"" 
            FROM 
                ""Products""");

        var conditions = new List<string>();
        
        if (minPrice.HasValue)
            conditions.Add(@"""Price"" >= @MinPrice");
        if (maxPrice.HasValue)
            conditions.Add(@"""Price"" <= @MaxPrice");
        
        if(conditions.Any())
            sql.Append(" WHERE " + string.Join(" AND ", conditions));
        
        sql.Append($" ORDER BY \"Price\" {direction}");

        return await connection
            .QueryAsync<Product>(
                sql.ToString(), new
                {
                    MinPrice = minPrice, 
                    MaxPrice = maxPrice
                }) 
                    ?? [];
    }

    public async Task<IEnumerable<Product>> FindSortedByRatingAsync(
        bool descending,
        CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);
        
        var direction = descending ? "DESC" : "ASC";
        
        var sql = new StringBuilder(@"
            SELECT 
                ""Id"", 
                ""ProductName"", 
                ""Price"", 
                ""Rating"", 
                ""ImageMiniPath"", 
                ""SellerName"" 
            FROM 
                ""Products""");

        sql.Append($" ORDER BY \"Rating\" {direction}");
        
        var data = sql.ToString();

        return await connection
            .QueryAsync<Product>(
                sql.ToString(),
                    cancellationToken)
                        ?? [];
    }

    public async Task<IEnumerable<Product>> SearchByNameAsync(
        string keyWord,
        CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
            SELECT 
                ""Id"", 
                ""ProductName"", 
                ""Price"", 
                ""Rating"", 
                ""ImageMiniPath"", 
                ""SellerName"" 
            FROM 
                ""Products""
            WHERE
                LOWER(""ProductName"") 
                    LIKE  @KeyWords
            OR 
               LOWER(""Description"")
                    LIKE @KeyWords
            ORDER BY 
                ""ProductName""";
        
            return await connection
                .QueryAsync<Product>(
                    sql, new
                    {
                        KeyWords = $"%{keyWord.ToLower()}%"
                    })
                        ?? [];
    }

    public async Task<IEnumerable<Product>> FindBySellerIdAsync(
        int sellerId,
        CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"
            SELECT*
            FROM ""Products""
            WHERE ""SellerId""=@SellerId";
        
        return await connection
           .QueryAsync<Product>(
               sql, new 
                   { SellerId = sellerId })
                       ?? [];
    }
    
    public async Task<IEnumerable<Product>> FindSortedByNoveltyAsync(
        bool descending,
        CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);
        
        var direction = descending ? "DESC" : "ASC";
        
        var sql = new StringBuilder(@"
            SELECT 
                ""Id"", 
                ""ProductName"", 
                ""Price"", 
                ""Rating"", 
                ""ImageMiniPath"", 
                ""SellerName"" 
            FROM 
                ""Products""");

        sql.Append($" ORDER BY \"CreatedDate\" {direction}");
        
        var data = sql.ToString();

        return await connection
           .QueryAsync<Product>(
               sql.ToString(),
               cancellationToken)
                   ?? [];
    }
}