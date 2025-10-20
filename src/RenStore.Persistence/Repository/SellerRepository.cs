using System.Text;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using RenStore.Application.Common.Exceptions;
using RenStore.Application.Repository;
using RenStore.Domain.Entities;
using RenStore.Persistence.SortedEnums;

namespace RenStore.Persistence.Repository;

public class SellerRepository 
{
    private readonly ApplicationDbContext _context;
    private readonly string _connectionString;
    private readonly Dictionary<SellerSortBy, string> _sortColumnMapping = 
        new()
        {
            { SellerSortBy.Id, "seller_id" },
            { SellerSortBy.Name, "seller_name" },
            { SellerSortBy.CreatedDate, "created_date" }
        };

    public SellerRepository(
        ApplicationDbContext context,
        string connectionString)
    {
        this._context = context;
        this._connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
    }

    public SellerRepository(
        ApplicationDbContext context,
        IConfiguration configuration)
    {
        this._context = context;
        this._connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    
    public async Task<long> CreateAsync(Seller seller, CancellationToken cancellationToken)
    {
        await this._context.Sellers.AddAsync(seller, cancellationToken);
        await this._context.SaveChangesAsync(cancellationToken);
        return seller.Id;
    }
    // TODO:
    public async Task UpdateAsync(Seller seller, CancellationToken cancellationToken)
    {
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var color = await this.GetByIdAsync(id, cancellationToken);
        this._context.Sellers.Remove(color);
        await this._context.SaveChangesAsync(cancellationToken);
    }
    /// <summary>
    /// Retrieves sellers with optional sorting, filtering by block status, and limiting the number of results.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <param name="sortedBy">Field to sort by (default: SellerId).</param>
    /// <param name="count">Maximum number of records to return (default: 25, max: 1000).</param>
    /// <param name="descending">Sort direction (true for descending, false for ascending).</param>
    /// <param name="isBlocked">Filter by block status (true for blocked, false for not blocked, null for all).</param>
    /// <returns>A collection of sellers.</returns>
    public async Task<IEnumerable<Seller>> FindAllAsync(
        CancellationToken cancellationToken,
        SellerSortBy sortedBy = SellerSortBy.Id,
        uint count = 25, 
        bool descending = false,
        bool? isBlocked = null)
    {
        try
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken);

            count = Math.Min(count, 1000);

            var columnName = _sortColumnMapping.GetValueOrDefault(sortedBy, "seller_id");

            string direction = descending ? "DESC" : "ASC";

            var parameters = new DynamicParameters();

            parameters.Add("Count", count);
        
            var sql = new StringBuilder(
                @"
                SELECT
                    ""seller_id"" AS Id,
                    ""seller_name"" AS Name,
                    ""normalized_seller_name"" AS NormalizedName,
                    ""seller_description"" AS Description,
                    ""created_date"" AS CreatedDate,
                    ""is_blocked"" AS IsBlocked,
                    ""user_id""  AS UserId
                FROM
                    ""sellers""
            ");

            if (isBlocked.HasValue)
            {
                sql.Append(@" WHERE ""is_blocked"" = @IsBlocked");
                parameters.Add("IsBlocked", isBlocked.Value);
            }
        
            sql.Append($" ORDER BY \"{columnName}\" {direction} LIMIT @Count;");
        
            return await connection
                .QueryAsync<Seller>(
                    sql.ToString(), 
                    parameters);
        }
        catch (NpgsqlException e)
        {
            throw new Exception($"Database error with seller {e.Message}");
        }
    }
    
    public async Task<Seller?> FindByIdAsync(long id, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = 
            @"
                SELECT
                    ""seller_id"" AS Id,
                    ""seller_name"" AS Name,
                    ""normalized_seller_name"" AS NormalizedName,
                    ""seller_description"" AS Description,
                    ""created_date"" AS CreatedDate,
                    ""is_blocked"" AS IsBlocked,
                    ""user_id""  AS UserId
                FROM
                    ""sellers""
                WHERE
                    ""seller_id"" = @id;
            ";
        
        return await connection
            .QueryFirstOrDefaultAsync<Seller>(
                sql, new { Id = id });
    }

    public async Task<Seller> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await this.FindByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException(typeof(Seller), id);
    }
    // TODO: make find all by name
    public async Task<IEnumerable<Seller>> FindByNameAsync(string name, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = 
            @"
                SELECT
                    ""seller_id"" AS Id,
                    ""seller_name"" AS Name,
                    ""normalized_seller_name"" AS NormalizedName,
                    ""seller_description"" AS Description,
                    ""created_date"" AS CreatedDate,
                    ""is_blocked"" AS IsBlocked,
                    ""user_id""  AS UserId
                FROM
                    ""sellers""
                WHERE
                    ""seller_name"" = @Name
                AND
                    ""normalized_seller_name""
                        LIKE  UPPER(@Name);
            ";
        
        return await connection
            .QueryAsync<Seller>(
                sql, new { Name = name })
                    ?? [];
    }
    
    public async Task<IEnumerable<Seller>> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await this.FindByNameAsync(name, cancellationToken)
            ?? throw new NotFoundException(typeof(Seller), name);
    }
    
    public async Task<Seller?> FindByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = 
            @"
                SELECT
                    ""seller_id"" AS Id,
                    ""seller_name"" AS Name,
                    ""normalized_seller_name"" AS NormalizedName,
                    ""seller_description"" AS Description,
                    ""created_date"" AS CreatedDate,
                    ""is_blocked"" AS IsBlocked,
                    ""user_id""  AS UserId
                FROM
                    ""sellers""
                WHERE
                    ""user_id"" = @UserId
            ";
        
        return await connection
            .QueryFirstOrDefaultAsync(
                sql, new { UserId = userId });
    }
    
    public async Task<Seller> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await this.FindByUserIdAsync(userId, cancellationToken)
            ?? throw new NotFoundException(typeof(Seller), userId);
    }   
    // TODO:
    public async Task<Seller?> FindByCreatedDateRangeAsync(CancellationToken cancellationToken)
    {
        return null;
    }
    // TODO:
    public async Task<Seller> GetByDateCreatedRangeAsync(CancellationToken cancellationToken)
    {
        return null;
    }
}